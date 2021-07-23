using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;
using Suzianna.WebDriver.Screenplay.Interactions;
using Xunit;

namespace Suzianna.WebDriver.Tests.Unit.Tests
{
    public class ClickOnElementsBySelectorTests
    {
        [Fact]
        public void click_on_a_single_element_with_no_delay()
        {
            var selector = By.Id("submit");
            var element = Substitute.For<IWebElement>();
            var driver = Substitute.For<IWebDriver>();
            driver.FindElement(selector).Returns(element);
            var browseTheWeb = BrowseTheWeb.With(driver);

            var actor = Actor.Named("jack").WhoCan(browseTheWeb);

            actor.AttemptsTo(Click.On(selector).WithNoDelay());

            element.Received(1).Click();
        }
    }
}
