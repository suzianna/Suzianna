using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions
{
    public class See<T> : Consequence<T>
    {
        private readonly IQuestion<T> _question;
        internal See(IQuestion<T> question)
        {
            _question = question;
        }
        protected override T Answer(Actor actor)
        {
            return _question.AnsweredBy(actor);
        }
    }
}
