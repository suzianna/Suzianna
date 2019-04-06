using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public class PutTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Put;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Put.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }
    }
}