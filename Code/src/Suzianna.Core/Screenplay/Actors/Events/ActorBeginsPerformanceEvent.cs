using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Events;

namespace Suzianna.Core.Screenplay.Actors.Events
{
    public class ActorBeginsPerformanceEvent : IEvent
    {
        public string ActorName { get; private set; }
        public ActorBeginsPerformanceEvent(string actorName)
        {
            this.ActorName = actorName;
        }
    }
}
