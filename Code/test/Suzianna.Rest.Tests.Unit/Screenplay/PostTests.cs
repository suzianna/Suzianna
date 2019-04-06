using System.Net.Http;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestUtils;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public class PostTests : HttpInteractionWithContentTests
    {
        protected override HttpMethod GetHttpMethod() => HttpMethod.Post;

        protected override HttpInteraction GetHttpInteraction(string resource)
        {
            return Post.DataAsJson(ContentFactory.SomeContent()).To(resource);
        }
    }
}
