using System.Net.Http;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Rest.Events
{
    public class StartSendingHttpRequestEvent : ISelfDescriptiveEvent
    {
        public StartSendingHttpRequestEvent(Actor actor, HttpRequestMessage message)
        {
            this.Method = message.Method;
            this.Url = message.RequestUri.AbsoluteUri;
            this.ActorName = actor.Name;
        }

        public string Url { get; private set; }
        public HttpMethod Method { get; private set; }
        public string ActorName { get; set; }
        public string Describe()
        {
            return $"{ActorName} is going to send a '{this.Method}' request to '{this.Url}'.";
        }
    }
}