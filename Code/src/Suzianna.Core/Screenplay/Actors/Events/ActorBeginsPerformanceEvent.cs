using Suzianna.Core.Events;

namespace Suzianna.Core.Screenplay.Actors.Events
{
    /// <summary>
    /// Event that indicates the actor is going to perform a <see cref="IPerformable"/>.
    /// </summary>
    public class ActorBeginsPerformanceEvent : ISelfDescriptiveEvent
    {
        /// <summary>
        /// Creates an instance of the event
        /// </summary>
        /// <param name="actorName">Name of the actor</param>
        public ActorBeginsPerformanceEvent(string actorName)
        {
            ActorName = actorName;
        }

        /// <summary>
        ///     Name of the actor
        /// </summary>
        public string ActorName { get; private set; }
        public string Describe()
        {
            return $"{ActorName} begins to perform.";
        }
    }
}