namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Post : HttpInteraction
    {
        public static Post DataAsJson(object content)
        {
            var post = new Post();
            post.RequestBuilder.WithPostVerb().WithContentAsJson(content);
            return post;
        }
        public Post To(string resource)
        {
            this.RequestBuilder.WithResourceName(resource);
            return this;
        }

        public static Post DataAsXml(object content)
        {
            var post = new Post();
            post.RequestBuilder.WithPostVerb().WithContentAsXml(content);
            return post;
        }
    }
}
