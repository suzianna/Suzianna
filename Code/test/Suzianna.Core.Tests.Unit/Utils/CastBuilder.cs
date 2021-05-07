using System.Collections.Generic;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.Core.Tests.Unit.Utils
{
    public class CastBuilder
    {
        private List<Actor> _actors;
        public CastBuilder()
        {
            _actors = new List<Actor>();
        }
        public CastBuilder WithActor(string name)
        {
            _actors.Add(new Actor(name));
            return this;
        }
        public Cast Build()
        {
            return new Cast(_actors.ToArray());
        }
    }
}
