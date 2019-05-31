using NFluent;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions
{
    public abstract class Consequence<T> : IConsequence<T>
    {
        public ICheck<T> AnsweredBy(Actor actor)
        {
            return Check.That(Answer(actor));
        }

        protected abstract T Answer(Actor actor);
    }
}