using Suzianna.Core.Screenplay.Questions;
using Suzianna.Http.Tests.Unit.TestDoubles;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Xunit;

namespace Suzianna.Http.Tests.Unit.Screenplay.QuestionTests
{
    public class LastResponseContentTests
    {
        private class Customer
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        [Fact]
        public void should_return_last_http_content()
        {
            var sender = new FakeHttpRequestSender();
            var response = "{firstname:'foo', lastname:'bar'}";
            var expectedCustomer = new Customer() { Firstname = "foo", Lastname = "bar"};
            sender.SetupResponse(new HttpResponseBuilder().WithContent(response).Build());
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);
            actor.AttemptsTo(Get.ResourceAt("api/resource"));

            actor.Should(See.That(Response.Content<Customer>())).HasPropertiesWithSameValues(expectedCustomer);
        }
    }
}
