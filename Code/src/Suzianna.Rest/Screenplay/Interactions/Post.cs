namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Post : HttpInteraction
    {
        private Post() { }
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
        public static Post Data(string content)
        {
            var post = new Post();
            post.RequestBuilder.WithPostVerb().WithContentAsPlainText(content);
            return post;
        }
    }
}
