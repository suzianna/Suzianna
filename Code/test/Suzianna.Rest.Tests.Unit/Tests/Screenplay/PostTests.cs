using System.Net.Http;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public class PostTests : HttpInteractionWithContentTests
    {
        private Actor actor;
        public PostTests()
        {
            actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);
        }
        protected override HttpMethod GetHttpMethod() => HttpMethod.Post;
        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Post.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }
        protected override HttpInteraction GetHttpInteractionWithJsonContent(object content)
        {
            return Post.DataAsJson(content).To(Urls.UsersApi);
        }
        protected override HttpInteraction GetHttpInteractionWithXmlContent(object content)
        {
            return Post.DataAsXml(content).To(Urls.UsersApi);
        }
        protected override HttpInteraction GetHttpInteractionWithCustomContent(string content)
        {
            return Post.Data(content).To(Urls.UsersApi);
        }
    }
}
