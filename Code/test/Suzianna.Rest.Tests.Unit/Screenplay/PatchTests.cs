using System.Net.Http;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Interactions;

namespace Suzianna.Http.Tests.Unit.Screenplay
{
    public class PatchTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Patch;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Patch.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }
    }
}
