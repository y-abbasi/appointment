namespace DevArt.Core.Domain.Constraints;

public record ReferentialIntegrityConstraint(string AggregateName, string AggregateId, string ErrorCode,
    string ErrorMessage) : IConstraint;