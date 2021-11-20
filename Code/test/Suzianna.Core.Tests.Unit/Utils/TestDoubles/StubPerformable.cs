using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Tests.Unit.Utils.TestDoubles
{
    public class StubPerformable : IPerformable
    {
        private Stack<Actor> _calledActors = new Stack<Actor>();
        public Task PerformAs<T>(T actor) where T : Actor
        {
            this._calledActors.Push(actor);
            return Task.CompletedTask;
        }

        public Actor LatestPerformer()
        {
            return _calledActors.LastOrDefault();
        }
        public long PerformTimes()
        {
            return _calledActors.Count;
        }
    }
}
