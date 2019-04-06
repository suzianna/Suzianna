using System.Collections.Generic;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Tests.Unit.Utils.TestDoubles;

namespace Suzianna.Core.Tests.Unit.Utils
{
    internal static class AbilityTestFactory
    {
        public static List<IAbility> GetSomeAbilities()
        {
            return new List<IAbility>()
            {
                new CallApiAbility(),
                new BrowseTheWebAbility()
            };
        }
    }
}
