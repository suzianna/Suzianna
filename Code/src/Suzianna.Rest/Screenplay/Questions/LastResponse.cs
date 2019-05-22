using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Suzianna.Core.Screenplay.Questions;

namespace Suzianna.Rest.Screenplay.Questions
{
    public static class LastResponse
    {
        public static IQuestion<HttpStatusCode> StatusCode() => new LastResponseStatusCode();
        public static IQuestion<T> Content<T>()=> new LastResponseTypedContent<T>();
        public static IQuestion<string> Header(string key)=> new LastResponseHeader(key);
        public static IQuestion<HttpResponseHeaders> Headers()=> new LastResponseHeaders();
        public static IQuestion<HttpResponseMessage> Raw()=> new LastResponseRaw();
    }
}
