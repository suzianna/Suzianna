using System.Net;
using System.Threading.Tasks;
using NFluent;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay.QuestionTests
{
    public class LastResponseStatusCodeTests : LastResponseContentTests
    {
        [Fact]
        public async Task should_return_last_http_status_code()
        {
            this.SetupResponse(new HttpResponseBuilder().WithHttpStatusCode(HttpStatusCode.NotFound).Build());

            await Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            Actor.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.NotFound);
        }
    }
}