using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Tests.Unit.Utils.TestDoubles
{
    public class StubPerformable : IPerformable
    {
        private Stack<Actor> _calledActors = new Stack<Actor>();
        public void PerformAs<T>(T actor) where T : Actor
        {
            this._calledActors.Push(actor);
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
