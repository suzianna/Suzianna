using System.Collections.Generic;
using System.Net.Http;
using Suzianna.Rest.Interception;

namespace Suzianna.Rest.Tests.Unit.TestDoubles
{
    public class FakeHttpInterceptor : IHttpInterceptor
    {
        private List<HttpRequestMessage> _interceptedHistory = new List<HttpRequestMessage>();
        private KeyValuePair<string, string> headerToAdd;

        private FakeHttpInterceptor(string header, string value)
        {
            headerToAdd = new KeyValuePair<string, string>(header, value);
        }

        public static FakeHttpInterceptor SetupToAddHeader(string key, string value)
        {
            return new FakeHttpInterceptor(key, value);
        }

        public HttpRequestMessage Intercept(HttpRequestMessage message)
        {
            message.Headers.Add(headerToAdd.Key, headerToAdd.Value);
            return message;
        }
    }
}