using Akka.Actor;
using Akka.DependencyInjection;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;

namespace DevArt.Core.Queries;

public class ProjectionManager<TProjection, TIdentity> : ReceiveActor
    where TProjection : ReceiveActor
    where TIdentity : IIdentifier
{
    public ProjectionManager()
    {
        Receive<EventMessage>( msg =>
        {
            var evt = (IDomainEvent<TIdentity>)msg.Event; 
            FindOrCreate(evt.TenantId).Forward(msg);
        });
    }

    protected virtual IActorRef FindOrCreate(TenantId tenantId)
    {
        var actorRef = Context.Child(tenantId.Value);

        if (actorRef.IsNobody())
        {
            actorRef = CreateProjectionActor(tenantId);
        }

        return actorRef;
    }

    protected virtual IActorRef CreateProjectionActor(TenantId tenantId)
    {
        var actorRef = Context.ActorOf(DependencyResolver.For(Context.System).Props<TProjection>(),
            tenantId.Value);
        Context.Watch(actorRef);
        return actorRef;
    }
}