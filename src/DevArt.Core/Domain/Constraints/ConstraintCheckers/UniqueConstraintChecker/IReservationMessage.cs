using DevArt.Core.Application;

namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public record ReservationMessageId(string Value) : IIdentifier
{
    public string PersistenceId => $"ReservationMessage-{Value}";
}
public interface IReservationMessage : IEntityMessage<ReservationMessageId>
{
    UniqueConstraint Constraint { get; }
}