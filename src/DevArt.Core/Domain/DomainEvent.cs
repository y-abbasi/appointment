using Akkatecture.Aggregates;
using DevArt.Core.IdentityAccess;
using Newtonsoft.Json;

namespace DevArt.Core.Domain;

public abstract record DomainEvent<TKey>(TKey AggregateId, string AggregateName, long Version) : IDomainEvent<TKey> where TKey : IValueObject
{
    [JsonProperty] public DateTime PublishedAt { get; private set; } = DateTime.Now;
    [JsonProperty] public UserId Publisher { get; private set; } = SecurityContext.Current.UserId;
    [JsonProperty] public TenantId TenantId { get; private set; } = SecurityContext.Current.TenantId;
}