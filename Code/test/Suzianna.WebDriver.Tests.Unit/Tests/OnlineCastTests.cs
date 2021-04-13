using NFluent;
using Suzianna.WebDriver.Screenplay.Actors;
using Suzianna.WebDriver.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.WebDriver.Tests.Unit.Tests
{
    public class OnlineCastTests
    {
        [Fact]
        public void an_online_cast_can_have_abilities_via_constructor()
        {
            var abilities = TestAbilityFactory.CreateSomeAbilities();

            var cast = new OnlineCast(abilities);

            Check.That(cast.Abilities).IsEqualTo(abilities);
        }

        [Fact]
        public void an_online_cast_can_have_abilities_via_static_factory()
        {
            var abilities = TestAbilityFactory.CreateSomeAbilities();

            var cast = OnlineCast.WhereEveryoneCan(abilities);

            Check.That(cast.Abilities).IsEqualTo(abilities);
        }


    }
}
