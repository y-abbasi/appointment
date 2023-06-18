using System.Security.Principal;
using Akkatecture.Aggregates;
using DevArt.Core.IdentityAccess;

namespace DevArt.Core.Domain;

public interface IDomainEvent
{
    DateTime PublishedAt { get; }
    string AggregateName { get; }
    UserId Publisher { get; }
    TenantId TenantId { get; }
    long Version { get; init; }
}

public interface IDomainEvent<out TKey> : IDomainEvent where TKey : IValueObject
{
    TKey AggregateId { get; }
}