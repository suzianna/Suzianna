using System.Linq;
using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class CastTests
    {
        [Fact]
        public void should_create_cast_with_abilities()
        {
            var abilities = AbilityTestFactory.GetSomeAbilities();

            var cast = new Cast(abilities);

            Check.That(cast.Abilities).IsEquivalentTo(abilities);
            Check.That(cast.Actors).IsEmpty();
        }

        [Fact]
        public void should_create_cast_with_abilities_using_whereEveryoneCan()
        {
            var abilities = AbilityTestFactory.GetSomeAbilities();

            var cast = Cast.WhereEveryoneCan(abilities);

            Check.That(cast.Abilities).IsEquivalentTo(abilities);
            Check.That(cast.Actors).IsEmpty();
        }

        [Fact]
        public void should_create_cast_with_actors()
        {
            var actors = ActorTestFactory.GetSomeActors();

            var cast = new Cast(actors);

            Check.That(cast.Actors).IsEquivalentTo(actors);
            Check.That(cast.Abilities).IsEmpty();
        }

        [Fact]
        public void should_add_actor_to_cast()
        {
            var cast = new Cast();

            cast.WithActorNamed(Names.Jack);

            Check.That(cast.Actors).CountIs(1).And.HasElementThatMatches(z => z.Name.Equals(Names.Jack));
        }

        [Fact]
        public void should_assign_abilities_of_cast_to_actors()
        {
            var abilities = AbilityTestFactory.GetSomeAbilities();
            var cast = new Cast(abilities);

            cast.WithActorNamed(Names.Jack);

            Check.That(cast.Actors).Not.IsEmpty();
            Check.That(cast.Actors.First().Abilities).IsEquivalentTo(abilities);
        }
    }
}
