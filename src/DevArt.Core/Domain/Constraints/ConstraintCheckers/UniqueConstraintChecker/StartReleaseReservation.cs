namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public record StartReleaseReservation(UniqueConstraint Constraint) : IReservationMessage
{
    public ReservationMessageId Id => new(Constraint.Key);
}