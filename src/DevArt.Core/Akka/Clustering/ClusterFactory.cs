using System.Linq.Expressions;
using System.Security.Principal;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Persistence;
using DevArt.Core.Application;
using DevArt.Core.Domain;
using DevArt.Core.Queries;

namespace DevArt.Core.Akka.Clustering;

public static class ClusterFactory<TActor, TIdentity>
    where TActor : ReceiveActor
    where TIdentity : IIdentifier
{
    public static IActorRef StartClusteredProjection(
        ActorSystem actorSystem,
        int numberOfShards = 12)
    {
        var clusterSharding = ClusterSharding.Get(actorSystem);
        var clusterShardingSettings = clusterSharding.Settings;

        var aggregateManagerProps = Props.Create(() => new ProjectionManager<TActor, TIdentity>());

        var shardRef = clusterSharding.Start(
            typeof(TActor).Name,
            Props.Create(() => new ClusterParentProxy(aggregateManagerProps, false)),
            clusterShardingSettings,
            new EventExtractor(numberOfShards)
        );

        return shardRef;
    }
 
    public static IActorRef StartClusteredAggregate(
        ActorSystem actorSystem,
        Expression<Func<TActor>> aggregateManagerFactory,
        int numberOfShards = 12)
    {
        var clusterSharding = ClusterSharding.Get(actorSystem);
        var clusterShardingSettings = clusterSharding.Settings;

        var aggregateManagerProps = Props.Create(aggregateManagerFactory);

        var shardRef = clusterSharding.Start(
            typeof(TActor).Name,
            Props.Create(() => new ClusterParentProxy(aggregateManagerProps, false)),
            clusterShardingSettings,
            new MessageExtractor<TIdentity>(numberOfShards)
        );

        return shardRef;
    }
}

public static class ClusterFactory<TAggregateManager, TAggregate, TIdentity, TCommand>
    where TAggregateManager : ReceivePersistentActor, IAggregateManager<TAggregate, TIdentity>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentifier
    where TCommand : IEntityMessage<TIdentity>
{
    public static IActorRef StartClusteredAggregate(
        ActorSystem actorSystem,
        int numberOfShards = 12)
    {
        var clusterSharding = ClusterSharding.Get(actorSystem);
        var clusterShardingSettings = clusterSharding.Settings;

        var aggregateManagerProps = Props.Create<ActorManager<TAggregateManager, TAggregate, TIdentity, TCommand>>();

        var shardRef = clusterSharding.Start(
            typeof(TAggregateManager).Name,
            Props.Create(() => new ClusterParentProxy(aggregateManagerProps, true)),
            clusterShardingSettings,
            new MessageExtractor<TIdentity>(numberOfShards)
        );

        return shardRef;
    }

    public static IActorRef StartClusteredAggregate(
        ActorSystem actorSystem,
        Expression<Func<TAggregateManager>> aggregateManagerFactory,
        int numberOfShards = 12)
    {
        var clusterSharding = ClusterSharding.Get(actorSystem);
        var clusterShardingSettings = clusterSharding.Settings;

        var aggregateManagerProps = Props.Create(aggregateManagerFactory);

        var shardRef = clusterSharding.Start(
            typeof(TAggregateManager).Name,
            Props.Create(() => new ClusterParentProxy(aggregateManagerProps, false)),
            clusterShardingSettings,
            new MessageExtractor<TIdentity>(numberOfShards)
        );

        return shardRef;
    }

    public static IActorRef StartAggregateClusterProxy(
        ActorSystem actorSystem,
        string clusterRoleName,
        int numberOfShards = 12)
    {
        var clusterSharding = ClusterSharding.Get(actorSystem);

        var shardRef = clusterSharding.StartProxy(
            typeof(TAggregateManager).Name,
            clusterRoleName,
            new MessageExtractor<TIdentity>(numberOfShards)
        );

        return shardRef;
    }
}