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
        public void should_return_minimum_value_of_answers()
        {
            var questions = QuestionTestFactory.CreateSomeQuestionsWithAnswers(40, 100, 50);
            var expectedAnswer = 40L;

            var actualAnswer = _actor.AsksFor(Min.Of(questions));

            Check.That(actualAnswer).IsEqualTo(expectedAnswer);
        }
        
        [Fact]
        public void should_return_minimum_of_results_on_complex_objects()
        {
            var hot = new Celsius(70);
            var cold = new Celsius(25);
            var hotQuestion = new StubQuestion<Celsius>().SetAnswer(hot);
            var coldQuestion = new StubQuestion<Celsius>().SetAnswer(cold);

            var answer = _actor.AsksFor(Min.Of(hotQuestion, coldQuestion));

            Check.That(answer).IsEqualTo(cold);
        }
        
        private class Celsius : IComparable<Celsius>
        {
            private byte Temperature { get; set; }
            public Celsius(byte temperature)
            {
                Temperature = temperature;
            }

            public int CompareTo(Celsius other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return this.Temperature.CompareTo(other.Temperature);
            }
        }
    }
}