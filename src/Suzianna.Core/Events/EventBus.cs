using System.Collections.Generic;

namespace Suzianna.Core.Events;

internal class EventBus : IEventBus
{
    private readonly List<IEventHandler> _subscribers = new();

    public void Publish<T>(T @event) where T : IEvent
    {
        foreach (var eventHandler in _subscribers) eventHandler.Handle(@event);
    }

    public void Subscribe(IEventHandler handler)
    {
        _subscribers.Add(handler);
    }
}