using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay.Questions
{
    public class SumQuestionTests
    {
        [Fact]
        public void should_sum_results_of_questions()
        {
            var jack = Actor.Named(Names.Jack);
            var questions = QuestionTestFactory.CreateSomeQuestionsWithAnswers<long>(10, 20);
            var sumQuestion = new SumQuestion(questions);
            var expectedAnswer = 30L;

            var actualAnswer = sumQuestion.AnsweredBy(jack);

            Check.That(actualAnswer).IsEqualTo(expectedAnswer);
        }
    }
}
