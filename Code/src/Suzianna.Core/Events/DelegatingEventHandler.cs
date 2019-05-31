using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core.Events
{
    public class DelegatingEventHandler : IEventHandler
    {
        private readonly Action<IEvent> _action;
        public DelegatingEventHandler(Action<IEvent> action)
        {
            this._action = action;
        }

        public void Handle(IEvent @event)
        {
            _action.Invoke(@event);
        }
    }
}
