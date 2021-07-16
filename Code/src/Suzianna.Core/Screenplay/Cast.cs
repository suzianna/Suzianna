using System.Collections.Generic;
using System.Linq;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay
{
    /// <summary>
    ///     A cast is a container for Screenplay actors.  It is useful in scenarios scenarios in which multiple actors are
    ///     involved.
    /// </summary>
    public class Cast
    {
        private readonly List<IAbility> _abilities;
        private readonly List<Actor> _actors;

        public IReadOnlyList<IAbility> Abilities => _abilities;
        public IReadOnlyList<Actor> Actors => _actors;

        /// <summary>
        ///     Creates a Cast object with a list of predefined abilities. Any actor added to this cast, will have these abilities.
        /// </summary>
        /// <param name="abilities">predefined abilities for a cast</param>
        public Cast(List<IAbility> abilities)
        {
            _abilities = abilities;
            _actors = new List<Actor>();
        }

        /// <summary>
        /// Creates a cast and assign actors to it.
        /// </summary>
        /// <param name="actors">Actors to be added to cast.</param>
        public Cast(params Actor[] actors)
        {
            _actors = actors.ToList();
            _abilities = new List<IAbility>();
        }

        /// <summary>
        ///     Creates a Cast object with a list of predefined abilities. Any actor added to this cast, will have these abilities.
        /// </summary>
        /// <param name="abilities">predefined abilities for a cast</param>
        public static Cast WhereEveryoneCan(List<IAbility> abilities)
        {
            return new Cast(abilities);
        }

        /// <summary>
        /// Adds an actor with to the cast. Actor added to the cast has predefined abilities of cast.
        /// </summary>
        /// <param name="name">name of actor</param>
        /// <returns>The actor that has added to the cast</returns>
        public Actor WithActorNamed(string name)
        {
            var actor = Actor.Named(name).WhoCan(_abilities);
            _actors.Add(actor);
            return actor;
        }
    }
}