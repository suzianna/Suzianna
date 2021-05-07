using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.OAuth;
using Suzianna.Rest.Screenplay.Questions;
using Suzianna.Rest.Tests.Unit.TestDoubles;
using Suzianna.Rest.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Rest.Tests.Unit.Tests.OAuth
{
    public class TokenParsingTests
    {
        private FakeHttpRequestSender _sender;
        private Actor _actor;
        public TokenParsingTests()
        {
            this._sender = new FakeHttpRequestSender();
            this._actor = ActorFactory.CreateSomeActorWithApiCallAbility(_sender);
        }

        [Fact]
        public void should_be_able_parse_token_response()
        {
            var content = "{" +
                          "\"access_token\":\"TOKEN_VALUE\"," +
                          "\"token_type\":\"JWT\"," +
                          "\"expires_in\":3600," +
                          "\"refresh_token\":\"tGzv3JOkF0XG5Qx2TlKWIA\"" +
                          "}";
            var response = new HttpResponseBuilder().WithContent(content).Build();
            _sender.SetupResponse(response);
            ActorRequestsForAccessToken();

            var token = _actor.AsksFor(LastResponse.Content<OAuthToken>());

            Check.That(token.AccessToken).IsEqualTo("TOKEN_VALUE");
            Check.That(token.TokenType).IsEqualTo("JWT");
            Check.That(token.ExpiresIn).IsEqualTo(3600);
            Check.That(token.RefreshToken).IsEqualTo("tGzv3JOkF0XG5Qx2TlKWIA");
        }

        private void ActorRequestsForAccessToken()
        {
            this._actor.AttemptsTo(GetAccessToken.UsingResourceOwnerPasswordCredentialFlow()
                .WithCredentials("Admin","123456")
                .FromEndpoint("http://localhost:5000"));
        }
    }
}
