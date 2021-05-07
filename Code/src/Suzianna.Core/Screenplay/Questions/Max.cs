using System;
using System.Collections.Generic;
using System.Linq;

namespace Suzianna.Core.Screenplay.Questions
{
    public static class Max
    {
        public static IQuestion<T> Of<T>(List<IQuestion<T>> questions) where T : IComparable<T>
        {
            return new MaximumQuestion<T>(questions);
        }
        public static IQuestion<T> Of<T>(params IQuestion<T>[] questions) where T : IComparable<T>
        {
            return new MaximumQuestion<T>(questions.ToList());
        }
    }
}