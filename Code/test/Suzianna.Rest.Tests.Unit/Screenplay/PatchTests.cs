using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Screenplay
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
