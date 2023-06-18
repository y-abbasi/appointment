using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;

namespace DevArt.Core.Application;

public abstract class AggregateManager<TAgg, TState, TKey, TCommand> : ReceivePersistentActor,
    IAggregateManager<TAgg, TKey>
    where TAgg : IAggregateRoot<TState, TKey>
    where TState : IAggregateState<TKey>
    where TKey : IIdentifier
{
    public override string PersistenceId { get; }
    private IMessage? _pinnedCommand = null;
    protected ILoggingAdapter Logger { get; }
    protected string Name { get; }

    protected AggregateManager(TAgg aggregate)
    {
        Logger = Context.GetLogger();
        Name = this.GetType().Name;
        Aggregate = aggregate;
        PersistenceId = aggregate.Id.PersistenceId;
        OnRecover();
        CommandAsync<TCommand>(Handle);
    }

    private void OnRecover()
    {
        Recover<RecoveryCompleted>(completed => { });
        RecoverAny(evt => { Aggregate.Process((IDomainEvent<TKey>)evt); });
    }

    private async Task<bool> Handle(TCommand cmd)
    {
        var arg = MapToArg(cmd);
        if (arg == null) return false;
        try
        {
            var events = await Aggregate.Do(arg);
            PersistAllAsync(events, evt =>
            {
                Context.System.EventStream.Publish(new EventMessage(PersistenceId, evt, 0, evt.Version,
                    evt.PublishedAt.Ticks));
                Sender.Tell(true);
            });
        }
        catch (Exception e)
        {
            Sender.Tell(e);
            throw;
        }

        return true;
    }

    protected abstract object? MapToArg(TCommand cmd);

    protected TAgg Aggregate { get; }

    protected override bool AroundReceive(Receive receive, object message)
    {
        if (message is IMessage msg)
        {
            _pinnedCommand = msg;
            SecurityContextSetter.SetTenantId(msg.TenantId);
            SecurityContextSetter.SetUserId(msg.UserId);
        }

        return base.AroundReceive(receive, message);
    }

    protected override SupervisorStrategy SupervisorStrategy()
    {
        var logger = Logger;
        return new OneForOneStrategy(
            maxNrOfRetries: 0,
            withinTimeMilliseconds: 3000,
            localOnlyDecider: x =>
            {
                logger.Warning("AggregateManager of Type={0}; will supervise Exception={1} to be decided as {2}.", Name,
                    x.ToString(), Directive.Stop);
                return Directive.Stop;
            });
    }
}

public interface IAggregateManager<TAgg, TKey>
{
}