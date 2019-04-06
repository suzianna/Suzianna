using System.Net.Http;
using Suzianna.Core.Screenplay;

namespace Suzianna.Rest.Screenplay.Abilities
{
    public class CallAnApi : IAbility
    {
        private IHttpRequestSender _sender;
        public string BaseUrl { get; private set; }
        public HttpResponseMessage LastResponse { get; private set; }
        private CallAnApi(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }
        public static CallAnApi At(string baseUrl)
        {
            return new CallAnApi(baseUrl);
        }
        public CallAnApi With(IHttpRequestSender sender)
        {
            this._sender = sender;
            return this;
        }
        public void SendRequest(HttpRequestMessage message)
        {
            this.LastResponse = this._sender.Send(message);
        }
    }
}
