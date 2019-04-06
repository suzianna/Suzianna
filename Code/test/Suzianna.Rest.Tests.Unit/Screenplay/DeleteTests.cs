using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public class DeleteTests : HttpInteractionTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Delete;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Delete.From(resource);
        }
    }
}