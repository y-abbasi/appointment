namespace DevArt.Core.Domain.Constraints;

public record UniqueConstraint(string AggregateName, string AggregateId, string Key, string ErrorCode,
    string ErrorMessage) : IConstraint;