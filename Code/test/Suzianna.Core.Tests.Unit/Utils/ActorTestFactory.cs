using Suzianna.Core.Screenplay;
using Suzianna.Core.Tests.Unit.Utils.Constants;

namespace Suzianna.Core.Tests.Unit.Utils
{
    internal static class ActorTestFactory
    {
        public static Actor[] GetSomeActors()
        {
            return new Actor[]
            {
                new Actor(Names.Jack),
                new Actor(Names.Victoria), 
            };
        }
    }
}
