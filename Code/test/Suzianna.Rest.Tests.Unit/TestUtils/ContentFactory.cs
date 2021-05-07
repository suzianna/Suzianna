namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    public static class ContentFactory
    {
        public static object SomeContent()
        {
            return new {a = "X"};
        }

        public static string AccessTokenRequest()
        {
            return "grant_type=password&username=admin&password=admin";
        }
    }
}