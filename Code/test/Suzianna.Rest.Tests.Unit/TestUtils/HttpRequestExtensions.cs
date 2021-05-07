using System.Linq;
using System.Net.Http;

namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    public static class HttpRequestExtensions
    {
        public static string FirstValueOfHeader(this HttpRequestMessage message, string headerKey)
        {
            return message.Headers.FirstOrDefault(a => a.Key == headerKey).Value.First();
        } 
        public static string SecondValueOfHeader(this HttpRequestMessage message, string headerKey)
        {
            return message.Headers.FirstOrDefault(a => a.Key == headerKey).Value.ElementAt(1);
        }

    }
}