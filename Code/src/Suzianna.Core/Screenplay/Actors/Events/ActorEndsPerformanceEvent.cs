using Suzianna.Core.Events;

namespace Suzianna.Core.Screenplay.Actors.Events
{
    public class ActorEndsPerformanceEvent : IEvent
    {
        public ActorEndsPerformanceEvent(string actorName)
        {
            ActorName = actorName;
        }

        public string ActorName { get; private set; }
    }
}