namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public record StartReservation(UniqueConstraint Constraint) : IReservationMessage
{
    public ReservationMessageId Id => new(Constraint.Key);
}