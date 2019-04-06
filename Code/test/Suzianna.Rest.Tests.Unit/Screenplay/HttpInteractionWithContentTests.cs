using System.Linq;
using FluentAssertions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public abstract class HttpInteractionWithContentTests : HttpInteractionTests
    {
        [Fact]
        public void should_send_request_content_headers()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi)
                .WithHeader(HttpHeaders.ContentType, MediaTypes.ApplicationJson)
                .WithHeader(HttpHeaders.ContentEncoding, ContentEncodings.GZip));

            Sender.GetLastSentMessage().Content.Headers.ContentType.MediaType.Should().Be(MediaTypes.ApplicationJson);
            Sender.GetLastSentMessage().Content.Headers.ContentEncoding.First().Should().Be(ContentEncodings.GZip);
        }
    }
}