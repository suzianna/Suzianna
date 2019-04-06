using System.Net.Http;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Interactions;

namespace Suzianna.Http.Tests.Unit.Screenplay
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