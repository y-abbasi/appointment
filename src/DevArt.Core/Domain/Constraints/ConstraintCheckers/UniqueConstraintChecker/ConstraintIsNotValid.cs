namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public record ConstraintIsNotValid;

record UniqueConstraintIsNotValid(UniqueConstraint Constraint) : ConstraintIsNotValid ,IReservationMessage
{
    public ReservationMessageId Id => new(Constraint.Key);
}