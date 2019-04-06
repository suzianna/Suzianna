using System.Net;
using NFluent;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Http.Tests.Unit.TestDoubles;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Xunit;

namespace Suzianna.Http.Tests.Unit.Screenplay.QuestionTests
{
    public class LastResponseStatusCodeTests
    {
        [Fact]
        public void should_return_last_http_status_code()
        {
            var sender = new FakeHttpRequestSender();
            sender.SetupResponse(new HttpResponseBuilder().WithHttpStatusCode(HttpStatusCode.NotFound).Build());
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);
            actor.AttemptsTo(Get.ResourceAt("api/resource"));

            actor.Should(See.That(Response.StatusCode())).IsEqualTo(HttpStatusCode.NotFound);
        }
    }
}
