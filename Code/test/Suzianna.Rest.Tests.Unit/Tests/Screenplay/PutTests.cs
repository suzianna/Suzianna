using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public class PutTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Put;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Put.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }
        protected override HttpInteraction GetHttpInteractionWithJsonContent(object content)
        {
            return Put.DataAsJson(content).To(Urls.UsersApi);
        }
        protected override HttpInteraction GetHttpInteractionWithXmlContent(object content)
        {
            return Put.DataAsXml(content).To(Urls.UsersApi);
        }

        protected override HttpInteraction GetHttpInteractionWithCustomContent(string content)
        {
            return Put.Data(content).To(Urls.UsersApi);
        }
    }
}