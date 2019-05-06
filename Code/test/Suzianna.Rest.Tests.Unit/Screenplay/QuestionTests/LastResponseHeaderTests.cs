using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using NFluent;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay.QuestionTests
{
    //TODO: needs refactoring
    public class LastResponseHeaderTests
    {
        [Fact]
        public void should_return_all_last_http_headers()
        {
            var sender = new FakeHttpRequestSender();
            var locationHeaderValue = "http://localhost:5000/api/resource/1";
            var originValue = "*";
            sender.SetupResponse(new HttpResponseBuilder()
                .WithHeader(HttpHeaders.Location, locationHeaderValue)
                .WithHeader(HttpHeaders.AccessControlAllowOrigin, originValue)
                .Build());
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);

            actor.AttemptsTo(Get.ResourceAt("api/resource"));

            actor.Should(See.That(LastResponse.Headers()))
                .HasElementThatMatches(a => a.Key == HttpHeaders.Location && a.Value.First() == locationHeaderValue)
                .And
                .HasElementThatMatches(a => a.Key == HttpHeaders.AccessControlAllowOrigin && a.Value.First() == originValue);
        }

        [Fact]
        public void should_return_last_http_headers_by_key()
        {
            var sender = new FakeHttpRequestSender();
            var locationHeaderValue = "http://localhost:5000/api/resource/1";
            sender.SetupResponse(new HttpResponseBuilder().WithHeader(HttpHeaders.Location, locationHeaderValue).Build());
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);

            actor.AttemptsTo(Post.DataAsJson(new { }).To("api/resource"));

            actor.Should(See.That(LastResponse.Header(HttpHeaders.Location))).IsEqualTo(locationHeaderValue);
        }
    }
}