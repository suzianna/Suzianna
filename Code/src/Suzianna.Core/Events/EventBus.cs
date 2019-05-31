using System;
using System.Collections.Generic;

namespace Suzianna.Core.Events
{
    internal class EventBus : IEventBus
    {
        private List<IEventHandler> _subscribers = new List<IEventHandler>();
        public void Publish<T>(T @event) where T : IEvent
        {
            foreach (var eventHandler in _subscribers)
            {
                eventHandler.Handle(@event);
            }
        }

        public void Subscribe(IEventHandler handler)
        {
            this._subscribers.Add(handler);
        }
    }
}