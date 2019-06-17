using System.Linq;
using System.Net.Http;
using FluentAssertions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public class PostTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Post;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Post.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }

        [Fact]
        public void should_post_data_as_json_with_appropriate_content_type_header()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(Post.DataAsJson(ContentFactory.SomeContent()).To(Urls.UsersApi));

            Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType.Should().Be(MediaTypes.ApplicationJson);
        }
    }
}
