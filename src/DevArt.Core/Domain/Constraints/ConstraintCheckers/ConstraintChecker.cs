using DevArt.Core.Akka;
using DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

namespace DevArt.Core.Domain.Constraints.ConstraintCheckers;

public class ConstraintChecker : IConstraintChecker
{
    private  Dictionary<string, IConstraintChecker> _mapper;

    public ConstraintChecker(ActorRefProvider<UniqueKeyManagerActor> manager)
    {
        _mapper = new()
        {
            { nameof(UniqueConstraint), new UniqueConstraintChecker.UniqueConstraintChecker(manager) }
        };
    }

    public Task<ITransaction> CheckAsync(IConstraint constraint)
    {
        return _mapper[constraint.GetType().Name].CheckAsync(constraint);
    }

    public Task<ITransaction> BeginReleaseAsync(IConstraint constraint)
    {
        return _mapper[constraint.GetType().Name].BeginReleaseAsync(constraint);
    }
}