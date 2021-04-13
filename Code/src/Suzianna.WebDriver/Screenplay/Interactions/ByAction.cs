using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public abstract class ByAction : WebInteraction
    {
        private readonly List<By> _locators;
        protected ByAction(params By[] locators)
        {
            if (locators == null) throw new ArgumentNullException(nameof(locators));
            if (locators.Length == 0) throw new ArgumentException("At least 1 locator is needed.");

            _locators = locators.ToList();
        }

        public IWebElement ResolveFor(Actor actor)
        {
            IWebElement element = null;
            var driver = GetDriver(actor);
            foreach (var locator in _locators)
            {
                element = (element == null) ? driver.FindElement(locator) : element.FindElement(locator);
            }
            //TODO: ensure that actor can see the element
            return element;
        }
    }
}
