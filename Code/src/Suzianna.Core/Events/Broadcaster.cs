using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Suzianna.Core.Events
{
    public static class Broadcaster
    {
        private static AsyncLocal<IEventBus> _bus = new AsyncLocal<IEventBus>();
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
}
