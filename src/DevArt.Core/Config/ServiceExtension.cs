using Akka.Actor;
using Akka.Persistence.EventStore;
using DevArt.Core.Akka;
using DevArt.Core.Application;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DevArt.Core.Config;

public class ShardCollection : Dictionary<Type, IActorRef>
{
}

public static class ServiceCollectionExtension
{
    public static void WireUpDevArtCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICommandDispatcher, ShardCommandDispatcher>();
        services.AddSingleton(new ShardCollection());
        services.AddSingleton(typeof(ActorRefProvider<>));
        
        services.AddSingleton<IEventStoreConnection>(provider =>
        {
            var settings = EventStorePersistence.Get(provider.GetRequiredService<ActorSystem>()).JournalSettings;
            var connectionString = settings.ConnectionString;
            var connectionName = settings.ConnectionName;
            var connection = EventStoreConnection
                .Create(connectionString, 
                    ConnectionSettings.Create().UseConsoleLogger().DisableTls(), $"{connectionName}.Read");
            connection.ConnectAsync().GetAwaiter().GetResult();
            return connection;
        });


        var moduleNames = configuration.GetSection("Modules").Get<ModuleSettings>();

        foreach (var type in moduleNames?.ModuleTypes.Select(Type.GetType)?? Type.EmptyTypes)
        {
            var module = Activator.CreateInstance(type) as IModule;
            module?.Register(services);
        }
    }
}

public interface IModule
{
    void Register(IServiceCollection services);
}

public class ModuleSettings
{
    public List<string> ModuleTypes { get; set; }
}