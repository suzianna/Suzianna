using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Suzianna.Core.Functional;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Suzianna.WebDriver.Pages.Targets
{
    public class ByTarget : Target
    {
        private By _locator;
        public ByTarget(string targetElementName, By locator) : this(targetElementName, locator, Maybe<TimeSpan>.None)
        {
        }

        public ByTarget(string targetElementName, By locator, Maybe<TimeSpan> timeout) : base(targetElementName, timeout)
        {
            this._locator = locator;
        }

        public override IWebElement ResolveFor(Actor actor)
        {
            var driver = actor.AbilityTo<BrowseTheWeb>().WebDriver;
            if (this.Timeout.HasValue)
            {
                var wait = new WebDriverWait(driver, this.Timeout.Value);
                return wait.Until(ExpectedConditions.ElementIsVisible(_locator));
            }
            return driver.FindElement(_locator);
        }
    }
}
