using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using Suzianna.Core.Events;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Actors.Events;
using Suzianna.Core.Tests.Unit.Tests.Utilities;
using Suzianna.Core.Tests.Unit.Utils.Constants;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;
using Xunit;

namespace Suzianna.Core.Tests.Unit.Tests.Screenplay
{
    public class ActorEventTests
    {
        private Actor actor;
        private ITask task;
        private Queue<IEvent> publishedEvents;
        public ActorEventTests()
        {
            this.actor = Actor.Named(Names.Jack);
            this.task = new DummyTask();
            this.publishedEvents = new Queue<IEvent>();
            Broadcaster.SubscribeToAllEvents(new DelegatingEventHandler(a=> publishedEvents.Enqueue(a)));
        }

        [Fact]
        public async Task should_raise_actor_raise_performance_begin_event_when_actor_performs_a_task()
        {
            await actor.AttemptsTo(task);

            Check.That(publishedEvents.First()).IsInstanceOf<ActorBeginsPerformanceEvent>();
            Check.That(publishedEvents.First().As<ActorBeginsPerformanceEvent>().ActorName).IsEqualTo(actor.Name);
        }

        [Fact]
        public async Task should_raise_actor_raise_performance_end_event_when_actor_performs_a_task()
        {
            await actor.AttemptsTo(task);

            Check.That(publishedEvents.Last()).IsInstanceOf<ActorEndsPerformanceEvent>();
            Check.That(publishedEvents.Last().As<ActorEndsPerformanceEvent>().ActorName).IsEqualTo(actor.Name);
        }

        private class DummyTask : ITask
        {
            public Task PerformAs<T>(T actor) where T : Actor { return Task.CompletedTask; }
        }
    }
}
