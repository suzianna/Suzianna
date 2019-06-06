using System.Collections.Generic;
using System.Net.Http;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Interception;

namespace Suzianna.Rest.Screenplay.Abilities
{
    public class CallAnApi : IAbility
    {
        private IHttpRequestSender _sender;
        private List<IHttpInterceptor> _interceptors;
        public string BaseUrl { get; private set; }
        public HttpResponseMessage LastResponse { get; private set; }
        private CallAnApi(string baseUrl)
        {
            this.BaseUrl = baseUrl;
            this._interceptors = new List<IHttpInterceptor>();
        }
        public static CallAnApi At(string baseUrl)
        {
            var ability =  new CallAnApi(baseUrl);
            return ability.With(new DefaultHttpRequestSender());
        }
        public CallAnApi With(IHttpRequestSender sender)
        {
            this._sender = sender;
            return this;
        }
        public CallAnApi WhichRequestsInterceptedBy(IHttpInterceptor interceptor)
        {
            this._interceptors.Add(interceptor);
            return this;
        }
        public void SendRequest(HttpRequestMessage message)
        {
            foreach (var interceptor in _interceptors)
            {
                message = interceptor.Intercept(message);
            }
            this.LastResponse = this._sender.Send(message);
        }
    }
}
