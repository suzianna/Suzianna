namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Delete : HttpInteraction
    {
        private Delete() { }
        public static Delete From(string resource)
        {
            var delete = new Delete();
            delete.RequestBuilder.WithDeleteVerb().WithResourceName(resource);
            return delete;
        }
        public Delete DataAsJson(object content)
        {
            RequestBuilder.WithDeleteVerb().WithContentAsJson(content);
            return this;
        }
    }
}
