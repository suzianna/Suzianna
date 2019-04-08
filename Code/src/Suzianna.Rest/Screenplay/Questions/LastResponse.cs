namespace Suzianna.Rest.Screenplay.Questions
{
    public static class LastResponse
    {
        public static LastResponseStatusCode StatusCode() => new LastResponseStatusCode();
        public static LastResponseTypedContent<T> Content<T>()=> new LastResponseTypedContent<T>();
    }
}
