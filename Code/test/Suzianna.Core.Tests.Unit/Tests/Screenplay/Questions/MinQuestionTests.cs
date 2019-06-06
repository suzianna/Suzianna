using System;
using NFluent;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay.Questions
{
    public class MinQuestionTests
    {
        private readonly Actor _actor;

        public MinQuestionTests()
        {
            _actor = Actor.Named(Names.Jack);
        }
        
        [Fact]
        public void should_return_maximum_value_of_answers()
        {
            var questions = QuestionTestFactory.CreateSomeQuestionsWithAnswers(40, 100, 50);
            var expectedAnswer = 40L;

            var actualAnswer = _actor.AsksFor(Min.Of(questions));

            Check.That(actualAnswer).IsEqualTo(expectedAnswer);
        }
        
        [Fact]
        public void should_return_maximum_of_results_on_complex_objects()
        {
            var oldMan = new Person(70);
            var youngMan = new Person(25);
            var oldManQuestion = new StubQuestion<Person>().SetAnswer(oldMan);
            var youngManQuestion = new StubQuestion<Person>().SetAnswer(youngMan);

            var answer = _actor.AsksFor(Min.Of(oldManQuestion, youngManQuestion));

            Check.That(answer).IsEqualTo(youngMan);
        }
        
        private class Person : IComparable<Person>
        {
            private long Age { get; set; }
            public Person(long age)
            {
                Age = age;
            }

            public int CompareTo(Person other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return Age.CompareTo(other.Age);
            }
        }
    }
}