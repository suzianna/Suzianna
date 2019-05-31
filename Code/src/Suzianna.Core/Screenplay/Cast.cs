using System.Collections.Generic;
using System.Linq;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay
{
    public class Cast
    {
        private List<IAbility> _abilities;
        private List<Actor> _actors;
        public IReadOnlyList<IAbility> Abilities => _abilities;
        public IReadOnlyList<Actor> Actors => _actors;

        public Cast(List<IAbility> abilities)
        {
            this._abilities = abilities;
            this._actors = new List<Actor>();
        }
        public Cast(params Actor[] actors)
        {
            this._actors = actors.ToList();
            this._abilities = new List<IAbility>();
        }

        public static Cast WhereEveryoneCan(List<IAbility> abilities)
        {
            return new Cast(abilities);
        }

        public Actor WithActorNamed(string name)
        {
            var actor = Actor.Named(name).WhoCan(this._abilities);
            this._actors.Add(actor);
            return actor;
        }
     
    }
}
