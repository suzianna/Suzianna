namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Put : HttpInteraction
    {
        public Put To(string resource)
        {
            this.RequestBuilder.WithResourceName(resource);
            return this;
        }

        public static Put DataAsJson(object content)
        {
            var put = new Put();
            put.RequestBuilder.WithPutVerb().WithContentAsJson(content);
            return put;
        }

        public static Put DataAsXml(object content)
        {
            var put = new Put();
            put.RequestBuilder.WithPutVerb().WithContentAsXml(content);
            return put;
        }
    }
}