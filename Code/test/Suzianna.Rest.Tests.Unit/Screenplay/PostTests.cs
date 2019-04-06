using System.Net.Http;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Interactions;

namespace Suzianna.Http.Tests.Unit.Screenplay
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
