namespace DevArt.Core.Domain.Constraints;

public interface IConstraint
{
    string AggregateName { get; }
    string ErrorCode { get; }
    string ErrorMessage { get; }
    string AggregateId { get; }
}