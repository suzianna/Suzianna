using System.Net.Http;

namespace Suzianna.Rest
{
    internal class DefaultHttpRequestSender : IHttpRequestSender
    {
        private static readonly HttpClient Client;

        static DefaultHttpRequestSender()
        {
            Client = new HttpClient();
        }

        public HttpResponseMessage Send(HttpRequestMessage message)
        {
            return Client.SendAsync(message).Result;
        }
    }
}