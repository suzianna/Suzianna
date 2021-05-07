using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public class DeleteTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Delete;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Delete.From(resource).DataAsJson(ContentFactory.SomeContent());
        }
    }
}