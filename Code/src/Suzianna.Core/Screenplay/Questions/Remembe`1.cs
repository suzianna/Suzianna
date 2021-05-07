using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions
{
    internal class Remember<T> : IQuestion<T>
    {
        private readonly string _key;
        internal Remember(string key)
        {
            _key = key;
        }
        public T AnsweredBy(Actor actor)
        {
            return actor.Recall<T>(this._key);
        }
    }
}