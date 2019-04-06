using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay.Questions
{
    public class SeeQuestionTests
    {
        [Fact]
        public void should_return_nfluent_check_based_on_underlying_question_result()
        {
            const long expectedAnswer = 10;
            var question = new StubQuestion<long>(expectedAnswer);
            var jack = Actor.Named(Names.Jack);

            jack.Should(See.That(question)).IsEqualTo(expectedAnswer);
        }
    }
}
