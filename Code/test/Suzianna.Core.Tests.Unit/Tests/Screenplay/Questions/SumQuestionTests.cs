using System;
using System.Collections;
using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay.Questions
{
    public class SumQuestionTests
    {
        [Fact]
        public void should_return_maximum_of_results_on_primitive_values()
        {
            var actor = Actor.Named(Names.Jack);
            var questions = QuestionTestFactory.CreateSomeQuestionsWithAnswers<long>(10, 20);
            var expectedAnswer = 30L;

            var actualAnswer = actor.AsksFor(Sum.Of(questions));

            Check.That(actualAnswer).IsEqualTo(expectedAnswer);
        }
        
       
    }
}
