using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Suzianna.Rest
{
    internal class DefaultHttpRequestSender : IHttpRequestSender
    {
        private static HttpClient _client;
        static DefaultHttpRequestSender()
        {
            _client = new HttpClient();
        }
        public HttpResponseMessage Send(HttpRequestMessage message)
        {
            return _client.SendAsync(message).Result;
        }
    }
}
