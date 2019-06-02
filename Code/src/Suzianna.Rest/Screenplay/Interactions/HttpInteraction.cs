using Suzianna.Core.Events;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Events;
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
            var request = this.RequestBuilder.WithBaseUrl(ability.BaseUrl).Build();
            Broadcaster.Publish(new StartSendingHttpRequestEvent(actor, request));
            ability.SendRequest(request);
            Broadcaster.Publish(new HttpRequestSentEvent(ability.LastResponse));
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
