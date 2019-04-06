namespace Suzianna.Rest.Screenplay.Interactions
{
    public class Get : HttpInteraction
    {
        private Get() { }
        public static Get ResourceAt(string resource)
        {
            var get = new Get();
            get.RequestBuilder.WithGetVerb().WithResourceName(resource);
            return get;
        }
    }
}
