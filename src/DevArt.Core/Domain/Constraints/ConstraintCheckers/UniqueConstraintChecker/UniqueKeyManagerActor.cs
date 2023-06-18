using Akka.Actor;
using Akka.DependencyInjection;

namespace DevArt.Core.Domain.Constraints.ConstraintCheckers.UniqueConstraintChecker;

public class UniqueKeyManagerActor : ReceiveActor
{
    public UniqueKeyManagerActor()
    {
        ReceiveAsync<IReservationMessage>(Handle);
    }

    private async Task Handle(IReservationMessage command)
    {
        var props = DependencyResolver.For(Context.System).Props<UniqueKeyReservationActor>(command.Constraint);
        await Context.Child(command.Constraint.Key)
            .GetOrElse(() => Context.ActorOf(props, command.Constraint.Key)).Ask(command, ConstraintCheckerSettings.Timeout)
            .PipeTo(Sender);
    }
}