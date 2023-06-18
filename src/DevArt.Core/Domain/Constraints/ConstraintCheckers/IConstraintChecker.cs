namespace DevArt.Core.Domain.Constraints.ConstraintCheckers;

public interface IConstraintChecker
{
    Task<ITransaction> CheckAsync(IConstraint constraint);
    Task<ITransaction> BeginReleaseAsync(IConstraint constraint);
}