using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestUtils;
using System.Threading.Tasks;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay.QuestionTests
{
    public class LastResponseContentTests : LastResponseTests
    {
        private class Customer
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        [Fact]
        public async Task should_return_last_http_content()
        {
            var response = "{firstname:'foo', lastname:'bar'}";
            this.SetupResponse(new HttpResponseBuilder().WithContent(response).Build());
            var expectedCustomer = new Customer { Firstname = "foo", Lastname = "bar" };

            await Actor.AttemptsTo(Get.ResourceAt("api/resource"));

            Actor.Should(See.That(LastResponse.Content<Customer>())).HasPropertiesWithSameValues(expectedCustomer);
        }
    }
}
