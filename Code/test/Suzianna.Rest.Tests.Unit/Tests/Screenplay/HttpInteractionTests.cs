using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NFluent;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Events;
using Suzianna.Rest.OAuth;
using Suzianna.Rest.Screenplay.Abilities;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.Screenplay
{
    public abstract class HttpInteractionTests
    {
        protected FakeHttpRequestSender Sender { get; } = new FakeHttpRequestSender();

        public static IEnumerable<object[]> GetUrlsWithRelativeResources()
        {
            return new List<object[]>
            {
                new object[] {"http://localhost:5050", "api/users", "http://localhost:5050/api/users"},
                new object[] {"http://localhost:5050", "/api/users", "http://localhost:5050/api/users"},
                new object[] {"http://localhost:5050/", "api/users", "http://localhost:5050/api/users"},
                new object[] {"http://localhost:5050/", "/api/users", "http://localhost:5050/api/users"},
                new object[] {"http://localhost:5050/api/", "users", "http://localhost:5050/api/users"}
            };
        }

        public static IEnumerable<object[]> GetUrlsWithAbsoluteResources()
        {
            return new List<object[]>
            {
                new object[]
                {
                    "http://localhost:5050", "http://localhost:5050/api/users", "http://localhost:5050/api/users"
                },
                new object[] {
                    "http://localhost:5050", "http://localhost:5050/api/users?id=10", "http://localhost:5050/api/users?id=10"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetUrlsWithRelativeResources))]
        [MemberData(nameof(GetUrlsWithAbsoluteResources))]
        public async Task should_send_request_to_correct_url(string baseUrl, string resource, string expectedUrl)
        {
            var juliet = Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(baseUrl).With(Sender));
            await juliet.AttemptsTo(GetHttpInteraction(resource));

            Check.That(Sender.GetLastSentMessage().RequestUri.AbsoluteUri).IsEqualTo(expectedUrl);
        }

        protected abstract HttpMethod GetHttpMethod();
        protected abstract HttpInteraction GetHttpInteraction(string resource);

        [Fact]
        public async Task should_add_query_string_when_resource_have_query_string_in_it()
        {
            var baseUrl = "http://localhost:5050";
            var resourceName = "api/users?page=2";
            var expectedUrl = "http://localhost:5050/api/users?page=2";
            var juliet = Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(baseUrl).With(Sender));

            await juliet.AttemptsTo(GetHttpInteraction(resourceName));

            Check.That(Sender.GetLastSentMessage().RequestUri.AbsoluteUri).IsEqualTo(expectedUrl);
        }

        [Fact]
        public async Task should_send_request_headers()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi)
                .WithHeader(HttpHeaders.Accept, MediaTypes.ApplicationXml));

            Check.That(Sender.GetLastSentMessage().Headers.Accept.First().MediaType).IsEqualTo(MediaTypes.ApplicationXml);
        }

        [Fact]
        public async Task should_send_request_to_url_with_query_parameters()
        {
            var baseUrl = "http://localhost:5050";
            var resourceName = "api/users";
            var expectedUrl = "http://localhost:5050/api/users?UserId=2&LocationId=3";
            var juliet = Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(baseUrl).With(Sender));

            await juliet.AttemptsTo(GetHttpInteraction(resourceName)
                .WithQueryParameter("UserId", "2")
                .WithQueryParameter("LocationId", "3"));

            Check.That(Sender.GetLastSentMessage().RequestUri.AbsoluteUri).IsEqualTo(expectedUrl);
        }

        [Fact]
        public async Task should_sent_correct_http_verb()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);

            await actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi));

            Check.That(Sender.GetLastSentMessage().Method).IsEqualTo(GetHttpMethod());
        }

        [Fact]
        public async Task should_use_query_string_both_from_resource_name_and_added_query_strings()
        {
            var baseUrl = "http://localhost:5050";
            var resourceName = "api/users?page=2";
            var expectedUrl = "http://localhost:5050/api/users?page=2&UserId=2&LocationId=3";
            var juliet = Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(baseUrl).With(Sender));

            await juliet.AttemptsTo(GetHttpInteraction(resourceName)
                .WithQueryParameter("UserId", "2")
                .WithQueryParameter("LocationId", "3"));

            Check.That(Sender.GetLastSentMessage().RequestUri.AbsoluteUri).IsEqualTo(expectedUrl);
        }

        [Fact]
        public async Task should_raise_start_sending_http_request_event()
        {
            var baseUrl = "http://localhost:5050";
            var resourceName = "api/users";
            var expectedUrl = "http://localhost:5050/api/users";
            var juliet = Actor.Named(Names.Juliet).WhoCan(CallAnApi.At(baseUrl).With(Sender));
            var publishedEvents = new Queue<IEvent>();
            Broadcaster.SubscribeToAllEvents(new DelegatingEventHandler(z=> publishedEvents.Enqueue(z)));

            await juliet.AttemptsTo(GetHttpInteraction(resourceName));

            Check.That(publishedEvents.CountOfType<StartSendingHttpRequestEvent>()).IsEqualTo(1);
            Check.That(publishedEvents.FirstElementOfType<StartSendingHttpRequestEvent>().Method).IsEqualTo(GetHttpMethod());
            Check.That(publishedEvents.FirstElementOfType<StartSendingHttpRequestEvent>().Url).IsEqualTo(expectedUrl);
            Check.That(publishedEvents.FirstElementOfType<StartSendingHttpRequestEvent>().ActorName).IsEqualTo(Names.Juliet);
        }

        [Fact]
        public async Task should_raise_http_request_sent_event()
        {
            Sender.SetupResponse(new HttpResponseBuilder()
                            .WithHttpStatusCode(HttpStatusCode.Accepted)
                            .WithContent(Contents.JackProfile)
                            .Build());

            var juliet = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);
            var publishedEvents = new Queue<IEvent>();
            Broadcaster.SubscribeToAllEvents(new DelegatingEventHandler(z => publishedEvents.Enqueue(z)));

            await juliet.AttemptsTo(GetHttpInteraction(""));

            Check.That(publishedEvents.CountOfType<HttpRequestSentEvent>()).IsEqualTo(1);
            Check.That(publishedEvents.FirstElementOfType<HttpRequestSentEvent>().ResponseCode).IsEqualTo(HttpStatusCode.Accepted);
            Check.That(publishedEvents.FirstElementOfType<HttpRequestSentEvent>().ResponseContent).IsEqualTo(Contents.JackProfile);
        }

        [Fact]
        public async Task should_send_request_with_authorization_header_when_actor_has_remembered_access_token()
        {
            var actor = ActorFactory.CreateSomeActorWithApiCallAbility(Sender);
            var token = OAuthTokenFactory.SomeToken();
            actor.Remember(TokenConstants.TokenKey, token);

            await actor.AttemptsTo(GetHttpInteraction(Urls.UsersApi));

            Check.That(Sender.GetLastSentMessage().Headers.Authorization.Scheme).IsEqualTo(token.TokenType);
            Check.That(Sender.GetLastSentMessage().Headers.Authorization.Parameter).IsEqualTo(token.AccessToken);
        }
    }
}