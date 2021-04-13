using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Suzianna.Core.Functional;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;

namespace Suzianna.WebDriver.Screenplay.Targets
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
            var driver = actor.FindAbility<BrowseTheWeb>().WebDriver;
            if (this.Timeout.HasValue)
            {
                var wait = new WebDriverWait(driver, this.Timeout.Value);
                return wait.Until(ExpectedConditions.ElementIsVisible(_locator));
            }
            return driver.FindElement(_locator);
        }
    }
}
