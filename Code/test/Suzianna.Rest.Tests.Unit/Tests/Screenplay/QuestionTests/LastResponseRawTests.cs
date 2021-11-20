using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public async Task should_return_last_response_as_raw_http()
        {
            var content = "{firstname:'foo', lastname:'bar'}";
            this.SetupResponse(new HttpResponseBuilder()
                .WithContent(content)
                .WithHttpStatusCode(HttpStatusCode.Accepted)
                .WithHeader(HttpHeaders.Warning, SampleHeaders.CacheDownWarning)
                .Build());

            await Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            var response = Actor.AsksFor(LastResponse.Raw());
            Check.That(Actor.AsksFor(LastResponse.Raw())).IsEqualTo(content);
            Check.That(Actor.AsksFor(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.Accepted);
            Check.That(Actor.AsksFor(LastResponse.Headers()).Warning.First().ToString()).IsEqualTo(SampleHeaders.CacheDownWarning);
        }
    }
}
