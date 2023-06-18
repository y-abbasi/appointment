using System.Data;
using Akka.Actor;
using Akka.Cluster;
using Akka.Configuration;
using Akka.DependencyInjection;
using Appointment.Application.Appointments;
using Appointment.Domain.Appointments;
using DevArt.Core.Akka.Clustering;
using DevArt.Core.Akka.Clustering.Configuration;
using DevArt.Core.Config;

namespace Appointment.RestApi;

public class AkkaService : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ShardCollection _shardActorRefs;
    private readonly IServiceProvider _provider;
    public static ActorSystem ActorSystem { get; private set; }

    public AkkaService(IHostApplicationLifetime lifetime, ShardCollection shardActorRefs, IServiceProvider provider)
    {
        _lifetime = lifetime;
        _shardActorRefs = shardActorRefs;
        _provider = provider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var path = Environment.CurrentDirectory;
        var configPath = Path.Combine(path, "app.conf");
        var config = ConfigurationFactory.ParseString(File.ReadAllText(configPath))
            .WithFallback(DevArtClusteringDefaultSettings.DefaultConfig());
        var bootstrap = BootstrapSetup.Create().WithConfig(config);
        var di = DependencyResolverSetup.Create(_provider);
        var actorSystemSetup = bootstrap.And(di);

        //Create actor system
        var clustername = config.GetString("akka.cluster.name");
        ActorSystem = ActorSystem.Create(clustername, actorSystemSetup);

        Cluster.Get(ActorSystem).RegisterOnMemberUp(() =>
        {
            _shardActorRefs[typeof(AppointmentAggregateActor)] =
                ClusterFactory<AppointmentAggregateActor, Domain.Appointments.Appointment, AppointmentId, IAppointmentCommand>
                    .StartClusteredAggregate(ActorSystem);
        });
        ActorSystem.WhenTerminated.ContinueWith(tr => { _lifetime.StopApplication(); }, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return ActorSystem.Terminate();
    }
}