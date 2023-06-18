using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using DevArt.Core.Domain;
using DevArt.Core.Domain.Constraints.ConstraintCheckers;
using DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;
using DevArt.Core.IdentityAccess;

namespace DevArt.Core.Application;

public abstract class AggregateManager<TAgg, TState, TKey, TCommand> : ReceivePersistentActor,
    IAggregateManager<TAgg, TKey>
    where TAgg : IAggregateRoot<TState, TKey>
    where TState : IAggregateState<TKey>
    where TKey : IIdentifier
{
    public override string PersistenceId { get; }
    private IMessage? _pinnedCommand = null;
    private readonly IConstraintChecker _constraintChecker;
    protected ILoggingAdapter Logger { get; }
    protected string Name { get; }

    protected AggregateManager(TAgg schemaId, IConstraintChecker constraintChecker)
    {
        Logger = Context.GetLogger();
        Name = this.GetType().Name;
        _constraintChecker = constraintChecker;
        SchemaId = schemaId;
        PersistenceId = schemaId.Id.PersistenceId;
        OnRecover();
        CommandAsync<TCommand>(Handle);
        Command<CheckConstraintIsValid>(cmd =>
        {
            if (SchemaId.AggregateState.GetConstraints().Contains(cmd.Constraint))
                Sender.Tell(SchemaId.AggregateState.GetConstraints().Contains(cmd.Constraint)
                    ? new ConstraintIsValid()
                    : new ConstraintIsNotValid());
        });
    }

    private void OnRecover()
    {
        Recover<RecoveryCompleted>(completed => { });
        RecoverAny(evt => { SchemaId.Process((IDomainEvent<TKey>)evt); });
    }

    private async Task<bool> Handle(TCommand cmd)
    {
        List<ITransaction> transactions = new();
        var beforeApplyCommand = SchemaId.GetConstraints().ToList();

        var arg = MapToArg(cmd);
        if (arg == null) return false;
        var events = await SchemaId.Do(arg);
        var afterApplyCommand = SchemaId.AggregateState.GetConstraints().ToList();
        try
        {
            foreach (var constraint in beforeApplyCommand.Except(afterApplyCommand))
                transactions.Add(await _constraintChecker.BeginReleaseAsync(constraint));
            foreach (var constraint in afterApplyCommand.Except(beforeApplyCommand))
                transactions.Add(await _constraintChecker.CheckAsync(constraint));
            PersistAllAsync(events, evt =>
            {
                Context.System.EventStream.Publish(new EventMessage(PersistenceId, evt, 0, evt.Version, evt.PublishedAt.Ticks));
                foreach (var transaction in transactions)
                    transaction.Commit().GetAwaiter().GetResult();
                Sender.Tell(true);
            });
        }
        catch (Exception e)
        {
            foreach (var transaction in transactions)
            {
                await transaction.Rollback();
            }

            Sender.Tell(e);
            throw;
        }

        return true;
    }

    protected abstract object? MapToArg(TCommand cmd);

    protected TAgg SchemaId { get; }

    protected override bool AroundReceive(Receive receive, object message)
    {
        if (message is IMessage msg)
        {
            _pinnedCommand = msg;
            SecurityContextSetter.SetTenantId(msg.TenantId);
            SecurityContextSetter.SetUserId(msg.UserId);
        }

        return base.AroundReceive(receive, message);
    }

    protected override SupervisorStrategy SupervisorStrategy()
    {
        var logger = Logger;
        return new OneForOneStrategy(
            maxNrOfRetries: 0,
            withinTimeMilliseconds: 3000,
            localOnlyDecider: x =>
            {
                logger.Warning("AggregateManager of Type={0}; will supervise Exception={1} to be decided as {2}.", Name,
                    x.ToString(), Directive.Stop);
                return Directive.Stop;
            });
    }
}

public interface IAggregateManager<TAgg, TKey>
{
}