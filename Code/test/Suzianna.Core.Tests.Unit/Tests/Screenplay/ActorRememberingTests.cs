using System;
using System.Collections.Generic;
using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class ActorRememberingTests
    {
        public ActorRememberingTests()
        {
            actor = Actor.Named(Names.Jack);
        }

        private readonly string key = "SOMEKEY";
        private readonly long value = 10;
        private readonly long secondValue = 20;
        private readonly Actor actor;

        [Fact]
        public void actor_should_answer_a_question_and_remember_the_answer()
        {
            var question = new StubQuestion<long>().SetAnswer(value);

            var answer = actor.Remember(key, question);
            var rememberedAnswer = actor.Recall<long>(key);

            Check.That(answer).IsEqualTo(value);
            Check.That(rememberedAnswer).IsEqualTo(value);
        }

        [Fact]
        public void actor_should_remember_and_recall_values()
        {
            actor.Remember(key, value);

            var actualValue = actor.Recall<long>(key);

            Check.That(actualValue).IsEqualTo(value);
        }

        [Fact]
        public void actor_should_throw_when_has_not_remembered_the_key_before()
        {
            Action remember = () => actor.Recall<long>(key);

            Check.ThatCode(remember).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void actor_should_throw_when_remembered_value_with_same_key_before()
        {
            actor.Remember(key, value);

            Action remember = () => actor.Remember(key, secondValue);

            Check.ThatCode(remember).Throws<ArgumentException>();
        }

        [Fact]
        public void should_recall_values_that_previously_remembered()
        {
            actor.Remember(key, value);

            actor.Should(See.That(Remember.ValueOf<long>(key))).IsEqualTo(value);
        }

        [Fact]
        public void actor_should_tell_if_he_can_recall_something()
        {
            actor.Remember(key, value);

            var canRecall = actor.CanRecall(key);

            Check.That(canRecall).IsTrue();
        }

        [Fact]
        public void actor_should_tell_if_he_can_not_recall_something()
        {
            var canRecall = actor.CanRecall(key);

            Check.That(canRecall).IsFalse();
        }
    }
}