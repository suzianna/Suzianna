namespace Suzianna.Core.Screenplay.Questions
{
    public static class See
    {
        public static See<T> That<T>(IQuestion<T> question)
        {
            return new See<T>(question);
        }
    }
}