using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Suzianna.Rest.Tests.Unit.TestDoubles
{
    public class FakeHttpRequestSender : IHttpRequestSender
    {
        private HttpResponseMessage _response = new HttpResponseMessage();
        private List<HttpRequestMessage> _sentMessages = new List<HttpRequestMessage>();
        public Task<HttpResponseMessage> Send(HttpRequestMessage message)
        {
            this._sentMessages.Add(message);
            return Task.FromResult(_response);
        }
        public IReadOnlyList<HttpRequestMessage> GetSentMessages() => this._sentMessages.AsReadOnly();
        public HttpRequestMessage GetLastSentMessage() => this._sentMessages.Last();
        public void SetupResponse(HttpResponseMessage message)
        {
            this._response = message;
        }
    }
}
