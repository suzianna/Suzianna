using System.Collections.Generic;

namespace Suzianna.Rest
{
    internal static class QueryStringUtil
    {
        public static List<KeyValuePair<string, string>> ExtractParameters(string uri)
        {
            var output = new List<KeyValuePair<string, string>>();
            var questionMarkIndex = uri.IndexOf('?');
            if (!HasQueryString(questionMarkIndex)) return output;

            var queryStringPart = uri.Substring(questionMarkIndex + 1, uri.Length - questionMarkIndex - 1);
            output.AddRange(ExtractQueryStringPairs(queryStringPart));
            return output;
        }

        private static bool HasQueryString(int questionMarkIndex)
        {
            return questionMarkIndex > -1;
        }

        private static List<KeyValuePair<string, string>> ExtractQueryStringPairs(string queryStringPart)
        {
            var output = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(queryStringPart))
            {
                var pairs = queryStringPart.Split('&');
                foreach (var pair in pairs)
                {
                    var parts = pair.Split('=');
                    var key = parts[0];
                    var value = parts[1];
                    output.Add(new KeyValuePair<string, string>(key, value));
                }
            }

            return output;
        }
    }
}