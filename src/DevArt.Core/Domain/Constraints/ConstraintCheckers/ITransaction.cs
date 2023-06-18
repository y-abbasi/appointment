namespace DevArt.Core.Domain.Constraints.ConstraintCheckers;

public interface ITransaction
{
    Task Commit();
    Task Rollback();
}