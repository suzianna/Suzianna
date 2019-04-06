using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class WriteCleanCode : IAbility { }

    public class StageTests
    {
        [Fact]
        public void should_be_created_with_cast()
        {
            var cast = new Cast();

            var stage = new Stage(cast);

            Check.That(stage.Cast).IsEqualTo(cast);
        }

        [Fact]
        public void should_shine_spotlight_on_actor()
        {
            var cast = new CastBuilder()
                            .WithActor(Names.Jack)
                            .WithActor(Names.Victoria)
                            .Build();
            var stage = new Stage(cast);

            stage.ShineSpotlightOn(Names.Jack);

            Check.That(stage.ActorInTheSpotlight).IsNotNull();
            Check.That(stage.ActorInTheSpotlight.Name).IsEqualTo(Names.Jack);
        }

        [Fact]
        public void should_add_actor_to_cast_and_shine_spotlight_on_him_when_actor_not_present_in_cast()
        {
            var cast = new CastBuilder().Build();
            var stage = new Stage(cast);

            stage.ShineSpotlightOn(Names.Jack);

            Check.That(stage.ActorInTheSpotlight).IsNotNull();
            Check.That(stage.ActorInTheSpotlight.Name).IsEqualTo(Names.Jack);
            Check.That(cast.Actors).CountIs(1).And.HasElementThatMatches(a => a.Name.Equals(Names.Jack));
        }
    }
}
