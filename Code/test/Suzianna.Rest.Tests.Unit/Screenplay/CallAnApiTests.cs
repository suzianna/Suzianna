using System.Linq;
using System.Net.Http;
using NFluent;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Screenplay
{
    public class CallAnApiTests
    {
        private readonly FakeHttpRequestSender _sender;
        private readonly HttpRequestMessage _request;
        public CallAnApiTests()
        {
            this._sender = new FakeHttpRequestSender();
            this._request = HttpRequestFactory.CreateRequest();
        }
        
        [Fact]
        public void should_set_base_url()
        {
            var callAnApi = CallAnApi.At(Urls.Google);

            Check.That(callAnApi.BaseUrl).IsEqualTo(Urls.Google);
        }

        [Fact]
        public void should_send_http_request_using_sender()
        {
            var callAnApi = CallAnApi.At(Urls.Google).With(_sender);

            callAnApi.SendRequest(_request);

            Check.That(_sender.GetLastSentMessage()).IsEqualTo(_request);
        }

        [Fact]
        public void should_intercept_requests_with_interceptors()
        {
            var interceptor = FakeHttpInterceptor.SetupToAddHeader(HttpHeaders.Authorization, Tokens.SomeToken);
            var callAnApi = CallAnApi.At(Urls.Google).With(_sender).WhichRequestsInterceptedBy(interceptor);

            callAnApi.SendRequest(_request);

            Check.That(_sender.GetLastSentMessage().FirstValueOfHeader(HttpHeaders.Authorization)).IsEqualTo(Tokens.SomeToken);
        }

        [Fact]
        public void should_intercept_requests_with_multiple_interceptors()
        {
            var tokenInterceptor = FakeHttpInterceptor.SetupToAddHeader(HttpHeaders.Authorization, Tokens.SomeToken);
            var acceptInterceptor = FakeHttpInterceptor.SetupToAddHeader(HttpHeaders.Accept, MediaTypes.ApplicationJson);
            var callAnApi = CallAnApi.At(Urls.Google).With(_sender)
                .WhichRequestsInterceptedBy(tokenInterceptor)
                .WhichRequestsInterceptedBy(acceptInterceptor);

            callAnApi.SendRequest(_request);

            Check.That(_sender.GetLastSentMessage().FirstValueOfHeader(HttpHeaders.Authorization)).IsEqualTo(Tokens.SomeToken);
            Check.That(_sender.GetLastSentMessage().FirstValueOfHeader(HttpHeaders.Accept)).IsEqualTo(MediaTypes.ApplicationJson);
        }
        
        [Fact]
        public void should_intercept_requests_in_order_of_registration()
        {
            const string sandbox = "Sandbox";
            var firstInterceptor = FakeHttpInterceptor.SetupToAddHeader(sandbox, "test");
            var secondInterceptor = FakeHttpInterceptor.SetupToAddHeader(sandbox, "test test");
            var callAnApi = CallAnApi.At(Urls.Google).With(_sender)
                .WhichRequestsInterceptedBy(firstInterceptor)
                .WhichRequestsInterceptedBy(secondInterceptor);

            callAnApi.SendRequest(_request);

            Check.That(_sender.GetLastSentMessage().FirstValueOfHeader(sandbox)).IsEqualTo("test");
            Check.That(_sender.GetLastSentMessage().SecondValueOfHeader(sandbox)).IsEqualTo("test test");
        }
    }
}
