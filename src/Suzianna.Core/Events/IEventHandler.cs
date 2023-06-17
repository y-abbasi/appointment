namespace Suzianna.Core.Events;

public interface IEventHandler
{
    void Handle(IEvent @event);
}