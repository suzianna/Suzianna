using System;
using System.Linq;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay
{
    /// <summary>
    /// Represent a scene where cast (actors) are performing a play.
    /// </summary>
    public class Stage
    {
        /// <summary>
        /// Cast that is performing on the stage
        /// </summary>
        /// <returns>Cast on the stage</returns>
        public Cast Cast { get; private set; }

        /// <summary>
        /// Gets the actor which is currently in the spotlight.
        /// </summary>
        /// <returns>Actor which is currently in the spotlight</returns>
        public Actor ActorInTheSpotlight { get;private set; }

        /// <summary>
        /// Creates a stage with specified cast.
        /// </summary>
        /// <param name="cast">cast of the stage</param>
        public Stage(Cast cast)
        {
            this.Cast = cast;
        }

        /// <summary>
        /// Sets the actor as the current actor in the spotlight. If actor does not exist in the cast, it creates the actor
        /// and adds it to the cast and sets it as the current actor in the spotlight.
        /// </summary>
        /// <param name="actorName">name of the actor</param>
        public void ShineSpotlightOn(string actorName)
        {
            var actor = FindActorInCast(actorName) ?? this.Cast.WithActorNamed(actorName);
            ActorInTheSpotlight = actor;
        }
        private Actor FindActorInCast(string actorName)
        {
            return this.Cast.Actors.FirstOrDefault(a=>a.Name.Equals(actorName, StringComparison.OrdinalIgnoreCase));
        }
    }
}