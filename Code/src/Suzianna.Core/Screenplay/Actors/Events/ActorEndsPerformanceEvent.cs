using Suzianna.Core.Events;

namespace Suzianna.Core.Screenplay.Actors.Events
{
    public class ActorEndsPerformanceEvent : ISelfDescriptiveEvent
    {
        public ActorEndsPerformanceEvent(string actorName)
        {
            ActorName = actorName;
        }

        public string ActorName { get; private set; }
        public string Describe()
        {
            return $"{ActorName} performance ended.";
        }
    }
}