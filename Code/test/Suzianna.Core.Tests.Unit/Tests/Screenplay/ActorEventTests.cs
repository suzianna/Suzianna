using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using NFluent;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Actors.Events;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class ActorEventTests
    {
        private Actor actor;
        private ITask task;
        private List<IEvent> publishedEvents;
        public ActorEventTests()
        {
            this.actor = Actor.Named(Names.Jack);
            this.task = new FakeTask();
            this.publishedEvents = new List<IEvent>();
            Broadcaster.SubscribeToAllEvents(new DelegatingEventHandler(a=> publishedEvents.Add(a)));
        }

        [Fact]
        public void should_raise_actor_begin_performance_event_when_actor_start_performing()
        {
            actor.AttemptsTo(task);

            Check.That(publishedEvents).CountIs(1);
            Check.That(publishedEvents.First()).IsInstanceOfType(typeof(ActorBeginsPerformanceEvent));
            Check.That(((ActorBeginsPerformanceEvent) publishedEvents.First()).ActorName).IsEqualTo(actor.Name);
        }
    }
}
