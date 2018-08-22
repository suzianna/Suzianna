using FluentAssertions;
using Suzianna.Core.Screenplay;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Screenplay
{
    public class ActorTests
    {
        private const string jack = "jack";

        [Fact]
        public void Constructor_should_construct_an_actor_with_proper_name()
        {
            var actor = new Actor(jack);

            actor.Name.Should().Be(jack);
        }

        [Fact]
        public void Named_should_create_an_actor_with_proper_name()
        {
            var actor = Actor.Named(jack);

            actor.Name.Should().Be(jack);
        }
    }
}
