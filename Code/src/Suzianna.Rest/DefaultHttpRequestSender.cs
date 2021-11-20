using System.Net.Http;
using System.Threading.Tasks;

namespace Suzianna.Rest
{
    public class DefaultHttpRequestSender : IHttpRequestSender
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DefaultHttpRequestSender(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<HttpResponseMessage> Send(HttpRequestMessage message)
        {
            var client = _httpClientFactory.CreateClient();
            return client.SendAsync(message);
        }
    }
}