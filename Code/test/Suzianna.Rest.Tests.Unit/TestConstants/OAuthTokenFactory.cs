using Suzianna.Rest.OAuth;

namespace Suzianna.Rest.Tests.Unit.TestConstants
{
    internal static class OAuthTokenFactory
    {
        public static OAuthToken SomeToken()
        {
            return new OAuthToken()
            {
                AccessToken = "ACCESS_TOKEN_VALUE",
                ExpiresIn = 3600,
                RefreshToken = "REFRESH_TOKEN_VALUE",
                TokenType = "JWT"
            };
        }
    }
}
