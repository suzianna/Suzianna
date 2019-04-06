using FluentAssertions;
using Suzianna.Http.Tests.Unit.TestConstants;
using Suzianna.Http.Tests.Unit.TestDoubles;
using Suzianna.Http.Tests.Unit.TestUtils;
using Suzianna.Rest.Screenplay.Abilities;
using Xunit;

namespace Suzianna.Http.Tests.Unit.Screenplay
{
    public class CallAnApiTests
    {
        [Fact]
        public void should_set_base_url()
        {
            var callAnApi = CallAnApi.At(Urls.Google);

            callAnApi.BaseUrl.Should().Be(Urls.Google);
        }

        [Fact]
        public void should_send_http_request_using_sender()
        {
            var sender = new FakeHttpRequestSender();
            var callAnApi = CallAnApi.At(Urls.Google).With(sender);
            var request = HttpRequestFactory.CreateRequest();

            callAnApi.SendRequest(request);

            sender.GetLastSentMessage().Should().Be(request);
        }
    }
}
