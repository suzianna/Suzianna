using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core.Events
{
    public interface IEventHandler
    {
        void Handle(IEvent @event);
    }
}
