using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;

namespace Suzianna.Core.Tests.Unit.Utils.TestDoubles
{
    public class StubQuestion<T> : IQuestion<T>
    {
        private T _answer;
        public StubQuestion()
        {
            _answer = default(T);
        }
        public StubQuestion(T answer)
        {
            SetAnswer(answer);
        }
        public StubQuestion<T> SetAnswer(T value)
        {
            this._answer = value;
            return this;
        }
        public T AnsweredBy(Actor actor)
        {
            return _answer;
        }
    }
}
