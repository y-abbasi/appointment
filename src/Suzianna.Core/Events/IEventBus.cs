﻿namespace Suzianna.Core.Events;

internal interface IEventBus
{
    void Publish<T>(T @event) where T : IEvent;
    void Subscribe(IEventHandler handler);
}