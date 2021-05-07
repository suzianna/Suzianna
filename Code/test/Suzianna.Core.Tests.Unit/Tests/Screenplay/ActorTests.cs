using System;
using NFluent;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Tests.Unit.Utils;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class ActorTests
    {
        [Fact]
        public void should_be_created_with_a_name()
        {
            var actor = new Actor(Names.Jack);

            Check.That(actor.Name).IsEqualTo(Names.Jack);
        }
        [Fact]
        public void should_be_created_with_a_name_via_named()
        {
            var actor = Actor.Named(Names.Jack);

            Check.That(actor.Name).IsEqualTo(Names.Jack);
        }

        [Fact]
        public void should_throw_when_actor_name_is_null()
        {
            Action constructor = () => new Actor(null);

            Check.ThatCode(constructor).Throws<ArgumentNullException>();
        }
       
        [Fact]
        public void should_be_able_to_enable_multiple_abilities_in_actor_via_whoCan()
        {
            var abilities = AbilityTestFactory.GetSomeAbilities();
            var actor = new Actor(Names.Jack);

            actor.WhoCan(abilities);

            Check.That(actor.Abilities).IsEqualTo(abilities);
        }

        [Fact]
        public void should_be_able_to_enable_multiple_abilities_in_actor()
        {
            var abilities = AbilityTestFactory.GetSomeAbilities();
            var actor = new Actor(Names.Jack);

            actor.Can(abilities);

            Check.That(actor.Abilities).IsEqualTo(abilities);
        }

        [Fact]
        public void should_perform_tasks()
        {
            var fetchUser = new StubPerformable();
            var saveUser = new StubPerformable();
            var jack = new Actor(Names.Jack);

            jack.AttemptsTo(fetchUser, saveUser);

            Check.That(fetchUser.LatestPerformer()).IsEqualTo(jack);
            Check.That(fetchUser.PerformTimes()).IsEqualTo(1);
            Check.That(saveUser.LatestPerformer()).IsEqualTo(jack);
            Check.That(saveUser.PerformTimes()).IsEqualTo(1);
        }

        [Fact]
        public void should_be_able_to_find_ability()
        {
            var callAnApi = new CallApiAbility();
            var actor = Actor.Named(Names.Jack).WhoCan(callAnApi);

            var ability = actor.FindAbility<CallApiAbility>();

            Check.That(ability).IsEqualTo(callAnApi);
        }

        [Fact]
        public void should_throw_when_actor_is_not_able()
        {
            var actor = Actor.Named(Names.Jack);

            Action findAbility = ()=> actor.FindAbility<CallApiAbility>();

            Check.ThatCode(findAbility).Throws<ActorIsUnableException<CallApiAbility>>();
        }

        [Fact]
        public void should_answer_the_question()
        {
            var actor = Actor.Named(Names.Jack);
            var expectedAnswer = 10;
            var question = new StubQuestion<long>(expectedAnswer);

            var answer = actor.AsksFor(question);

            Check.That(answer).IsEqualTo(expectedAnswer);
        }

        [Fact]
        public void should_be_able_to_enable_an_ability_in_actor()
        {
            var callApi = new CallApiAbility();
            var actor = new Actor(Names.Jack);

            actor.Can(callApi);

            ActorShouldHaveThisAbilityOnly(actor, callApi);
        }
        [Fact]
        public void should_be_able_to_enable_an_ability_in_actor_via_whoCan()
        {
            var callApi = new CallApiAbility();

            var actor = Actor.Named(Names.Jack).WhoCan(callApi);

            ActorShouldHaveThisAbilityOnly(actor, callApi);
        }
        private static void ActorShouldHaveThisAbilityOnly(Actor actor, IAbility ability)
        {
            Check.That(actor.Abilities).Contains(ability).And.CountIs(1);
        }
    }
}
