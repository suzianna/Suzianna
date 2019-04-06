using System.Net.Http;
using Suzianna.Rest.Tests.Unit.TestConstants;

namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    public static class HttpRequestFactory
    {
        public static HttpRequestMessage CreateRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, Urls.Google);
        }
    }
}
