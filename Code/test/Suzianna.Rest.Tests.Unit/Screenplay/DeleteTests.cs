using System.Linq;
using System.Net.Http;
using FluentAssertions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay
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