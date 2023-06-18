using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Event;
using Akka.Persistence;
using DevArt.Core.Application;
using DevArt.Core.Domain;
using DevArt.Core.Extensions;

namespace DevArt.Core.Akka;

public class ActorManager<TAggregateMgr, TAggregate, TIdentity, TCommand> : ReceiveActor
    where TAggregateMgr : ReceivePersistentActor, IAggregateManager<TAggregate, TIdentity>
    where TIdentity : IIdentifier
    where TCommand : IEntityMessage<TIdentity>
{
    protected ILoggingAdapter Logger { get; set; }
    public string Name { get; }

    public ActorManager()
    {
        Logger = Context.GetLogger();
        Name = GetType().PrettyPrint();
        Receive<Terminated>(Terminate);

        Receive<TCommand>(Dispatch);
    }

    protected virtual bool Dispatch(TCommand command)
    {
        Logger.Info("AggregateManager of Type={0}; has received a command of Type={1}", Name,
            command.GetType().PrettyPrint());

        var aggregateRef = FindOrCreate(command.Id);

        aggregateRef.Forward(command);

        return true;
    }

    protected virtual bool Terminate(Terminated message)
    {
        Logger.Warning("Aggregate of Type={0}, and Id={1}; has terminated.", typeof(TAggregateMgr).PrettyPrint(),
            message.ActorRef.Path.Name);
        Context.Unwatch(message.ActorRef);
        return true;
    }

    protected virtual IActorRef FindOrCreate(TIdentity aggregateId)
    {
        var aggregate = Context.Child(aggregateId.PersistenceId);

        if (aggregate.IsNobody())
        {
            aggregate = CreateAggregate(aggregateId);
        }

        return aggregate;
    }

    protected virtual IActorRef CreateAggregate(TIdentity aggregateId)
    {
        var aggregateRef = Context.ActorOf(DependencyResolver.For(Context.System).Props<TAggregateMgr>(aggregateId), aggregateId.PersistenceId);
        Context.Watch(aggregateRef);
        return aggregateRef;
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