using System.Linq;
using System.Net;
using NFluent;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay.QuestionTests
{
    public class LastResponseRawTests : LastResponseTests
    {
        [Fact]
        public void should_return_last_response_as_raw_http()
        {
            var content = "{firstname:'foo', lastname:'bar'}";
            this.SetupResponse(new HttpResponseBuilder()
                .WithContent(content)
                .WithHttpStatusCode(HttpStatusCode.Accepted)
                .WithHeader(HttpHeaders.Warning, SampleHeaders.CacheDownWarning)
                .Build());

            Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            var response = Actor.AsksFor(LastResponse.Raw());
            Check.That(response.Content.ReadAsStringAsync().Result).IsEqualTo(content);
            Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.Accepted);
            Check.That(response.Headers.Warning.First().ToString()).IsEqualTo(SampleHeaders.CacheDownWarning);
        }
    }
}
