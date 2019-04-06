namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Delete : HttpInteraction
    {
        public static Delete From(string resource)
        {
            var delete =new Delete();
            delete.RequestBuilder.WithDeleteVerb().WithResourceName(resource);
            return delete;
        }
    }
}
