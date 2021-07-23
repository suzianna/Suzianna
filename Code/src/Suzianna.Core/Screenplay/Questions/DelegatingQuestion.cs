using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions
{
    internal class DelegatingQuestion<T> : IQuestion<T>
    {
        private readonly Func<Actor, T> _func;
        public DelegatingQuestion(Func<Actor, T> func)
        {
            _func = func;
        }

        public T AnsweredBy(Actor actor)
        {
            return _func.Invoke(actor);
        }
    }
}
