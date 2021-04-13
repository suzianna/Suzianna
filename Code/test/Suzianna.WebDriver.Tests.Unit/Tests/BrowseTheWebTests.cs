using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using Suzianna.WebDriver.Screenplay.Abilities;
using Suzianna.WebDriver.Tests.Unit.TestDoubles;
using Xunit;

namespace Suzianna.WebDriver.Tests.Unit.Tests
{
    public class BrowseTheWebTests
    {
        [Fact]
        public void ability_to_browse_the_web_needs_a_web_driver()
        {
            var driver = new SpyWebDriver();

            var ability = BrowseTheWeb.With(driver);

            Check.That(ability.WebDriver).IsEqualTo(driver);
        }
    }
}
