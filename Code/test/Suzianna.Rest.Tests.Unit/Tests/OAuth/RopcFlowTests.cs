using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using NFluent;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.OAuth;
using Suzianna.Rest.Tests.Unit.TestConstants;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.OAuth
{
    public class RopcFlowTests
    {
        private FakeHttpRequestSender sender;
        private Actor actor;
        public RopcFlowTests()
        {
            this.sender = new FakeHttpRequestSender();
            actor = ActorFactory.CreateSomeActorWithApiCallAbility(sender);
        }

        [Fact]
        public void should_send_request_to_correct_url()
        {
            const string endpoint = "http://localhost:5000/";
            const string username = "admin";
            const string password = "123456";
            SetupSuccessfulResponse(OAuthTokenFactory.SomeToken());

            actor.AttemptsTo(GetAccessToken
                .UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials(username, password)
                .FromEndpoint(endpoint));

            var lastRequestContent = sender.GetLastSentMessage().RequestUri.AbsoluteUri;
            Check.That(lastRequestContent).IsEqualTo(endpoint);
        }

        [Fact]
        public void should_send_username_and_password()
        {
            const string endpoint = "http://localhost:5000/";
            const string username = "admin";
            const string password = "123456";
            const string expected = "grant_type=password&username=admin&password=123456";
            SetupSuccessfulResponse(OAuthTokenFactory.SomeToken());

            actor.AttemptsTo(GetAccessToken
                .UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials(username, password)
                .FromEndpoint(endpoint));

            var lastRequestContent = sender.GetLastSentMessage().Content.ReadAsStringAsync().Result;
            Check.That(lastRequestContent).IsEqualTo(expected);
        }

        [Fact]
        public void should_send_scope()
        {
            const string endpoint = "http://localhost:5000/";
            const string username = "admin";
            const string password = "123456";
            const string scope = "read-emails";
            const string expected = "grant_type=password&username=admin&password=123456&scope=read-emails";
            SetupSuccessfulResponse(OAuthTokenFactory.SomeToken());

            actor.AttemptsTo(GetAccessToken
                .UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials(username, password)
                .WithScope(scope)
                .FromEndpoint(endpoint));

            var lastRequestContent = sender.GetLastSentMessage().Content.ReadAsStringAsync().Result;
            Check.That(lastRequestContent).IsEqualTo(expected);
        }

        [Fact]
        public void actor_should_remember_access_token_after_response()
        {
            const string endpoint = "http://localhost:5000/";
            const string username = "admin";
            const string password = "123456";
            var token = OAuthTokenFactory.SomeToken();
            SetupSuccessfulResponse(token);

            actor.AttemptsTo(GetAccessToken
                .UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials(username, password)
                .FromEndpoint(endpoint));

            var rememberedToken = actor.Recall<OAuthToken>(TokenConstants.TokenKey);

            Check.That(rememberedToken).HasFieldsWithSameValues(token);
        }

        [Fact]
        public void actor_not_remember_anything_when_response_is_not_success()
        {
            const string endpoint = "http://localhost:5000/";
            const string username = "admin";
            const string password = "123456";
            sender.SetupResponse(new HttpResponseBuilder().WithHttpStatusCode(HttpStatusCode.BadRequest).Build());

            actor.AttemptsTo(GetAccessToken
                .UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials(username, password)
                .FromEndpoint(endpoint));

            Action remembering =()=> actor.Recall<OAuthToken>(TokenConstants.TokenKey);

            Check.ThatCode(remembering).Throws<KeyNotFoundException>();
        }

        private void SetupSuccessfulResponse(OAuthToken token)
        {
            var content = JsonConvert.SerializeObject(token);
            sender.SetupResponse(new HttpResponseBuilder().WithContent(content).Build());
        }
    }
}
