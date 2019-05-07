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
    public class LastResponseHeaderTests : LastResponseTests
    {
        [Fact]
        public void should_return_all_last_http_headers()
        {
            var locationHeaderValue = "http://localhost:5000/api/resource/1";
            var originValue = "*";
            this.SetupResponse(new HttpResponseBuilder()
                .WithHeader(HttpHeaders.Location, locationHeaderValue)
                .WithHeader(HttpHeaders.AccessControlAllowOrigin, originValue)
                .Build());

            Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            //TODO: check to find a better way
            Actor.Should(See.That(LastResponse.Headers()))
                .HasElementThatMatches(a => a.Key == HttpHeaders.Location && a.Value.First() == locationHeaderValue)
                .And
                .HasElementThatMatches(a => a.Key == HttpHeaders.AccessControlAllowOrigin && a.Value.First() == originValue);
        }

        [Fact]
        public void should_return_last_http_headers_by_key()
        {
            var locationHeaderValue = "http://localhost:5000/api/resource/1";
            this.SetupResponse(new HttpResponseBuilder().WithHeader(HttpHeaders.Location, locationHeaderValue).Build());

            Actor.AttemptsTo(Post.DataAsJson(new { }).To("api/resource"));

            Actor.Should(See.That(LastResponse.Header(HttpHeaders.Location))).IsEqualTo(locationHeaderValue);
        }
    }
}