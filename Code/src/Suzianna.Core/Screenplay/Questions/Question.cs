using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Screenplay.Questions
{
    public static class Question
    {
        public static IQuestion<T> From<T>(Func<Actor, T> func)
        {
            return new DelegatingQuestion<T>(func);
        }
    }
}
