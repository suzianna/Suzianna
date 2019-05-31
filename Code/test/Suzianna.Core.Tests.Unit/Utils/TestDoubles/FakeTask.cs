using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Tests.Unit.Utils.TestDoubles
{
    public class FakeTask : ITask
    {
        public void PerformAs<T>(T actor) where T : Actor
        {

        }
    }
}
