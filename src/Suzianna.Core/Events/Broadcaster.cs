using System.Threading;

namespace Suzianna.Core.Events;

public static class Broadcaster
{
    private static readonly AsyncLocal<IEventBus> _bus = new();

    public static void Publish<T>(T @event) where T : IEvent
    {
        GetBus().Publish(@event);
    }

    public static void SubscribeToAllEvents(IEventHandler handler)
    {
        GetBus().Subscribe(handler);
    }

    private static IEventBus GetBus()
    {
        if (_bus.Value == null)
            _bus.Value = new EventBus();
        return _bus.Value;
    }
}