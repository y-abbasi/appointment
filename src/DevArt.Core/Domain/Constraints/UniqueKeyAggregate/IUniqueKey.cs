using DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

namespace DevArt.Core.Domain.Constraints.UniqueKeyAggregate;

// public interface IUniqueKey : IAggregateRoot<UniqueKeyId>,
//     IDo<StartReservation, UniqueKeyId>,
//     IDo<StartReleaseReservation, UniqueKeyId>,
//     IDo<StartReleaseReservation, UniqueKeyId>,
// {
// }

public record UniqueKeyId(string AggregateName, string KeyValue) : IIdentifier
{
    public string Value => $"{AggregateName}-{KeyValue}";
    public string PersistenceId => $"{AggregateName}-{KeyValue}";
}