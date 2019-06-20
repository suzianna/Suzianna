using System.IO;

namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Patch : HttpInteraction
    {
        public static Patch DataAsJson(object content)
        {
            var patch = new Patch();
            patch.RequestBuilder.WithPatchVerb().WithContentAsJson(content);
            return patch;
        }
        public Patch To(string resource)
        {
            this.RequestBuilder.WithResourceName(resource);
            return this;
        }

        public static Patch DataAsXml(object content)
        {
            var patch = new Patch();
            patch.RequestBuilder.WithPatchVerb().WithContentAsXml(content);
            return patch;
        }

        public static Patch Data(string content)
        {
            var patch = new Patch();
            patch.RequestBuilder.WithPatchVerb().WithContentAsPlainText(content);
            return patch;
        }
    }
}
