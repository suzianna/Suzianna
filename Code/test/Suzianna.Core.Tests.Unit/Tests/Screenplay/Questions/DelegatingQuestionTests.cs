using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay.Questions
{
    public class DelegatingQuestionTests
    {
        [Fact]
        public void calls_the_delegate_on_answering_by_actor()
        {
            var actor = Actor.Named("jack");
            var question = Question.From(a => 10);

            var result = question.AnsweredBy(actor);

            Check.That(result).IsEqualTo(10);
        }
    }
}
