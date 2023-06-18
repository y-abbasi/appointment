using Akka.Actor;
using Akka.Persistence;

namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public class UniqueKeyReservationActor : ReceivePersistentActor
{
    private UniqueConstraint _constraint;
    private UniqueKeyReservationState _state;
    private string _upcomingOwnerAggregateId = "";

    public UniqueKeyReservationActor(UniqueConstraint constraint)
    {
        _constraint = constraint;
        PersistenceId = $"{constraint.AggregateName}-{constraint.Key}";
        Recover<StartReservation>(_ =>
        {
            _upcomingOwnerAggregateId = _.Constraint.AggregateId;
            Become(TransientState);
        });
        Recover<CompletelyReserved>(_ => Become(Reserved));
        Recover<CompletelyReleased>(_ => Become(Released));
        Recover<StartReleaseReservation>(_ => { Become(TransientState); });
        Recover<RecoveryCompleted>(_ =>
        {
            if (_state is UniqueKeyReservationState.Transient)
                CheckConstraintSourceIsValid();
        });
        Become(Released);
    }

    private void CheckConstraintSourceIsValid()
    {
        Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self,
            new CheckConstraintIsValid(_constraint), Self);
    }

    private void Released()
    {
        _state = UniqueKeyReservationState.Released;
        Command<StartReservation>(reserve =>
        {
            _upcomingOwnerAggregateId = reserve.Constraint.AggregateId;
            Become(TransientState);
            Persist(reserve, _ => Sender.Tell(true, Self));
        });
        CommandAny(_ => Sender.Tell(false, Self));
    }

    private void Reserved()
    {
        _state = UniqueKeyReservationState.Reserved;
        _constraint = _constraint with { AggregateId = _upcomingOwnerAggregateId };
        Command<StartReservation>(_ => { Sender.Tell(_.Constraint.AggregateId == _constraint.AggregateId, Self); });
        Command<StartReleaseReservation>(cancelled =>
        {
            Become(TransientState);
            Persist(cancelled, _ =>  Sender.Tell(true, Self));
        });
    }

    private void TransientState()
    {
        _state = UniqueKeyReservationState.Transient;
        Command<StartReservation>(_ => Stash.Stash());
        Command<StartReleaseReservation>(_ => Stash.Stash());
        Command<ConstraintIsNotValid>(confirmed =>
        {
            
            Become(Released);
            Persist(new CompletelyReleased(), _ =>
            {
                Stash.UnstashAll();
                Sender.Tell(true);
            });
        });
        Command<ConstraintIsValid>(cancelled =>
        {
            Become(Reserved);
            Persist(new CompletelyReserved(), _ =>
            {
                Stash.UnstashAll();
                Sender.Tell(true);
            });
        });
        CheckConstraintSourceIsValid();
    }

    public override string PersistenceId { get; }

    private record CompletelyReserved;
    private record CompletelyReleased;
}

internal enum UniqueKeyReservationState
{
    Released,
    Reserved,
    Transient
}