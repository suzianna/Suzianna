using System;
using System.Linq;

namespace Suzianna.Core.Screenplay
{
    public class Stage
    {
        public Cast Cast { get; private set; }
        public Actor ActorInTheSpotlight { get;private set; }
        public Stage(Cast cast)
        {
            this.Cast = cast;
        }
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