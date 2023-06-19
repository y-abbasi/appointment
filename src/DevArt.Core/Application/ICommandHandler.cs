using Akka.Actor;
using Akka.DependencyInjection;
using DevArt.Core.Akka;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DevArt.Core.Application;

public class AggregateAttribute : Attribute
{
    public string AggregateName { get; }

    public AggregateAttribute(string aggregateName)
    {
        AggregateName = aggregateName;
    }
}

public interface ICommandDispatcher

{
    Task Dispatch<TApp, TKey>(ICommand<TKey> command) where TKey : IValueObject where TApp : ActorBase;
    Task<TResult> Request<TApp, TKey, TResult>(ICommand<TKey> command) where TKey : IValueObject where TApp : ActorBase;
}

public class ShardCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _provider;

    public ShardCommandDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Dispatch<TApp, TKey>(ICommand<TKey> command) where TKey : IValueObject where TApp : ActorBase
    {
        var result = await _provider.GetService<ActorRefProvider<TApp>>().Ask(command);
        if (result is Exception ex)
            throw ex;
    }

    public async Task<TResult> Request<TApp, TKey, TResult>(ICommand<TKey> command) where TApp : ActorBase where TKey : IValueObject
    {
        return await _provider.GetService<ActorRefProvider<TApp>>().Ask<TResult>(command);
    }
}

public interface IMessage
{
    UserId UserId { get; }
    TenantId TenantId { get; }
}

public interface IEntityMessage<out TKey>
{
    TKey Id { get; }
}
public interface ICommand<out TKey> : IMessage, IEntityMessage<TKey>
{
}