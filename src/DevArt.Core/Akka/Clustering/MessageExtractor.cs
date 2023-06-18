using Akka.Cluster.Sharding;
using DevArt.Core.Application;
using DevArt.Core.Domain;

namespace DevArt.Core.Akka.Clustering;

public class MessageExtractor<TIdentity> : HashCodeMessageExtractor where TIdentity : IIdentifier
{
    public MessageExtractor(int maxNumberOfShards) 
        : base(maxNumberOfShards)
    {
    }

    public override string EntityId(object message)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        if (message is IEntityMessage<TIdentity> command)
            return command.Id.Value;

        throw new ArgumentException(nameof(message));
    }
}