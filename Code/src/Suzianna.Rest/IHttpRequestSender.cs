using System.Net.Http;
using System.Threading.Tasks;

namespace Suzianna.Rest
{
    public interface IHttpRequestSender
    {
        Task<HttpResponseMessage> Send(HttpRequestMessage message);
    }
}
