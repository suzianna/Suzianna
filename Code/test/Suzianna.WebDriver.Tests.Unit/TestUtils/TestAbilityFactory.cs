using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay;
using Suzianna.WebDriver.Tests.Unit.TestDoubles;

namespace Suzianna.WebDriver.Tests.Unit.TestUtils
{
    public static class TestAbilityFactory
    {
        public static List<IAbility> CreateSomeAbilities()
        {
            return new List<IAbility>()
            {
                new DummyAbility(),
            };
        }
    }
}
