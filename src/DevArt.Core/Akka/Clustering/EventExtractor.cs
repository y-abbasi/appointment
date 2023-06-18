using Akka.Cluster.Sharding;
using DevArt.Core.Domain;

namespace DevArt.Core.Akka.Clustering;

public class EventExtractor : HashCodeMessageExtractor
{
    public EventExtractor(int maxNumberOfShards)
        : base(maxNumberOfShards)
    {
    }

    public override string EntityId(object message)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        if (message is EventMessage { Event: IDomainEvent dev }) return dev.TenantId.Value;

        throw new ArgumentException(nameof(message));
    }
}