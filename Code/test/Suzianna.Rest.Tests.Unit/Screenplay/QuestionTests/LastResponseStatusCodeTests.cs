using System.Net;
using NFluent;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay.QuestionTests
{
    public class LastResponseStatusCodeTests : LastResponseContentTests
    {
        [Fact]
        public void should_return_last_http_status_code()
        {
            this.SetupResponse(new HttpResponseBuilder().WithHttpStatusCode(HttpStatusCode.NotFound).Build());

            Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            Actor.Should(See.That(LastResponse.StatusCode())).IsEqualTo(HttpStatusCode.NotFound);
        }
    }
}