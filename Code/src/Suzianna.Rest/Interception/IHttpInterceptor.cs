using System.Net.Http;

namespace Suzianna.Rest.Interception
{
    public interface IHttpInterceptor
    {
        HttpRequestMessage Intercept(HttpRequestMessage message);
    }
}