using System.Net.Http;
using Suzianna.Http.Tests.Unit.TestConstants;

namespace Suzianna.Http.Tests.Unit.TestUtils
{
    public static class HttpRequestFactory
    {
        public static HttpRequestMessage CreateRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, Urls.Google);
        }
    }
}
