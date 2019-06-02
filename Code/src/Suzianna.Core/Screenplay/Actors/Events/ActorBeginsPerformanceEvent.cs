using Suzianna.Core.Events;

namespace Suzianna.Core.Screenplay.Actors.Events
{
    public class ActorBeginsPerformanceEvent : ISelfDescriptiveEvent
    {
        public ActorBeginsPerformanceEvent(string actorName)
        {
            ActorName = actorName;
        }

        public string ActorName { get; private set; }
        public string Describe()
        {
            return $"{ActorName} begins to perform.";
        }
    }
}