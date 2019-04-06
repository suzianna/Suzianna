using System.Collections.Generic;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;

namespace Suzianna.Core.Tests.Unit.Utils
{
    internal class QuestionTestFactory
    {
        public static IQuestion<T> CreateQuestion<T>()
        {
            return new StubQuestion<T>();
        }
        public static List<IQuestion<T>> CreateSomeQuestions<T>()
        {
            var questions = new List<IQuestion<T>>()
            {
                new StubQuestion<T>(),
                new StubQuestion<T>(),
            };
            return questions;
        }
        public static List<IQuestion<T>> CreateSomeQuestionsWithAnswers<T>(params T[] answers)
        {
            var questions = new List<IQuestion<T>>();
            foreach (var answer in answers)
            {
                questions.Add(new StubQuestion<T>(answer));;
            }
            return questions;
        }
    }
}
