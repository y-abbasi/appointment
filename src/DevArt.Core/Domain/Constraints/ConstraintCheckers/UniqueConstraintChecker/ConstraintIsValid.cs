namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public record ConstraintIsValid;

record UniqueConstraintIsValid(UniqueConstraint Constraint) : ConstraintIsValid ,IReservationMessage
{
    public ReservationMessageId Id => new(Constraint.Key);
}