using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;
using System.Net.Http;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public class DeleteTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Delete;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Delete.From(resource).DataAsJson(ContentFactory.SomeContent());
        }

        protected override HttpInteraction GetHttpInteractionWithJsonContent(object content)
        {
            return Delete.From(Urls.UsersApi).DataAsJson(content);
        }

        protected override HttpInteraction GetHttpInteractionWithXmlContent(object content)
        {
            return Delete.From(Urls.UsersApi).DataAsXml(content);
        }

        protected override HttpInteraction GetHttpInteractionWithCustomContent(string content)
        {
            return Delete.From(Urls.UsersApi).Data(content);
        }
    }
}