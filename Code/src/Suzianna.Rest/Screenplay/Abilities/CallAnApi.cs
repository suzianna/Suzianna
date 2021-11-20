using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Interception;

namespace Suzianna.Rest.Screenplay.Abilities
{
    public class CallAnApi : IAbility
    {
        private IHttpRequestSender _sender;
        private List<IHttpInterceptor> _interceptors;
        
        public string BaseUrl { get; private set; }
        public string LastResponseContent { get; private set; }
        public HttpStatusCode LastResponseStatusCode { get; private set; }
        public HttpResponseHeaders LastResponseHeaders { get; private set; }

        private CallAnApi(string baseUrl)
        {
            this.BaseUrl = baseUrl;
            this._interceptors = new List<IHttpInterceptor>();
        }
        public static CallAnApi At(string baseUrl, IHttpRequestSender requestSender = null)
        {            
            var ability =  new CallAnApi(baseUrl);
            return ability.With(requestSender);
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
        public async Task SendRequest(HttpRequestMessage message)
        {
            foreach (var interceptor in _interceptors)
            {
                message = interceptor.Intercept(message);
            }
                        
            using (var lastResponse = await _sender.Send(message))
            {
                LastResponseContent = string.Empty;
                if (lastResponse.Content != null)
                {
                    LastResponseContent = await lastResponse.Content?.ReadAsStringAsync();
                }
                LastResponseHeaders = lastResponse.Headers;
                LastResponseStatusCode = lastResponse.StatusCode;
            }
        }
    }
}
