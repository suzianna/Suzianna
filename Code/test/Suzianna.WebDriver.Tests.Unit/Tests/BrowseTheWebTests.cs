using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NFluent;
using NSubstitute;
using OpenQA.Selenium;
using Suzianna.WebDriver.Screenplay.Abilities;
using Xunit;

namespace Suzianna.WebDriver.Tests.Unit.Tests
{
    public class BrowseTheWebTests
    {
        [Fact]
        public void browsing_the_web_is_powered_by_a_driver()
        {
            var driver = Substitute.For<IWebDriver>();

            var browseTheWebAbility = BrowseTheWeb.With(driver);

            Check.That(browseTheWebAbility.WebDriver).IsEqualTo(driver);
        }
    }
}
