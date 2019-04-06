using System.Collections.Generic;

namespace Suzianna.Rest
{
    internal static class AuthorizationHeaderFactory
    {
        internal static KeyValuePair<string,string> Create(string tokenValue, TokenTypes tokenTypes)
        {
            var value = GetHeaderValue(tokenValue, tokenTypes);
            return new KeyValuePair<string, string>(HttpHeaders.Authorization, value);
        }

        private static string GetHeaderValue(string tokenValue, TokenTypes tokenType)
        {
            var tokenTypeName = tokenType.ToString();
            return $"{tokenTypeName} {tokenValue}";
        }

    }
}