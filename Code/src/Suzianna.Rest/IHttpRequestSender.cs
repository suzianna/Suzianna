using System.Net.Http;

namespace Suzianna.Rest
{
    public interface IHttpRequestSender
    {
        HttpResponseMessage Send(HttpRequestMessage message);
    }
}
