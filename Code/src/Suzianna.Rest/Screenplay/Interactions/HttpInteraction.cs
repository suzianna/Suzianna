using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Screenplay.Abilities;

namespace Suzianna.Rest.Screenplay.Interactions
{
    public abstract class HttpInteraction : IInteraction
    {
        internal HttpRequestBuilder RequestBuilder { get; private set; }
        protected HttpInteraction()
        {
            this.RequestBuilder = new HttpRequestBuilder();    
        }
        public void PerformAs<T>(T actor) where  T : Actor
        {
            var ability = actor.FindAbility<CallAnApi>();
            this.RequestBuilder.WithBaseUrl(ability.BaseUrl);
            ability.SendRequest(this.RequestBuilder.Build());
        }
        public HttpInteraction WithQueryParameter(string key, string value)
        {
            this.RequestBuilder.WithQueryParameter(key, value);
            return this;
        }
        public HttpInteraction WithHeader(string key, string value)
        {
            this.RequestBuilder.WithHeader(key, value);
            return this;
        }
    }
}
