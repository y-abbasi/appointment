using DevArt.Core.Akka;
using DevArt.Core.ErrorHandling;

namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public class UniqueConstraintChecker : IConstraintChecker
{
    private readonly ActorRefProvider<UniqueKeyManagerActor> _uniqueKeyReservationActor;

    public UniqueConstraintChecker(ActorRefProvider<UniqueKeyManagerActor> uniqueKeyReservationActor)
    {
        _uniqueKeyReservationActor = uniqueKeyReservationActor;
    }

    public async Task<ITransaction> CheckAsync(IConstraint constraint)
    {
        if (!await _uniqueKeyReservationActor.Ask<bool>(new StartReservation((UniqueConstraint)constraint),
                ConstraintCheckerSettings.Timeout))
            throw new BusinessException(constraint.ErrorCode, constraint.ErrorMessage);
        return new UniqueKeyReservationTransaction(_uniqueKeyReservationActor, (UniqueConstraint)constraint);
    }

    public async Task<ITransaction> BeginReleaseAsync(IConstraint constraint)
    {
        await _uniqueKeyReservationActor.Ask(new StartReleaseReservation((UniqueConstraint)constraint),
            ConstraintCheckerSettings.Timeout);
        return new UniqueKeyReleaseReservationTransaction(_uniqueKeyReservationActor, (UniqueConstraint)constraint);
    }
}

public class UniqueKeyReservationTransaction : ITransaction
{
    private readonly ActorRefProvider<UniqueKeyManagerActor> _uniqueKeyReservationActor;
    private readonly UniqueConstraint _constraint;

    public UniqueKeyReservationTransaction(ActorRefProvider<UniqueKeyManagerActor> uniqueKeyReservationActor,
        UniqueConstraint constraint)
    {
        _uniqueKeyReservationActor = uniqueKeyReservationActor;
        _constraint = constraint;
    }

    public async Task Commit()
    {
        await _uniqueKeyReservationActor.Ask(new UniqueConstraintIsValid(_constraint));
    }

    public async Task Rollback()
    {
        await _uniqueKeyReservationActor.Ask(new UniqueConstraintIsNotValid(_constraint));
    }
}

public class UniqueKeyReleaseReservationTransaction : ITransaction
{
    private readonly ActorRefProvider<UniqueKeyManagerActor> _uniqueKeyReservationActor;
    private readonly UniqueConstraint _constraint;

    public UniqueKeyReleaseReservationTransaction(ActorRefProvider<UniqueKeyManagerActor> uniqueKeyReservationActor,
        UniqueConstraint constraint)
    {
        _uniqueKeyReservationActor = uniqueKeyReservationActor;
        _constraint = constraint;
    }

    public async Task Commit()
    {
        await _uniqueKeyReservationActor.Ask(new UniqueConstraintIsNotValid(_constraint),
            ConstraintCheckerSettings.Timeout);
    }

    public async Task Rollback()
    {
        await _uniqueKeyReservationActor.Ask(new UniqueConstraintIsValid(_constraint),
            ConstraintCheckerSettings.Timeout);
    }
}