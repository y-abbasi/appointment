namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.ReferentialIntegrityChecker;

public class ReferentialIntegrityConstraintChecker : IConstraintChecker
{
    public Task<ITransaction> CheckAsync(IConstraint constraint)
    {
        throw new NotImplementedException();
    }

    public Task<ITransaction> BeginReleaseAsync(IConstraint constraint)
    {
        throw new NotImplementedException();
    }
}