using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public abstract class ByAction : WebInteraction
    {
        private readonly By _locator;
        protected ByAction(By locator)
        {
            _locator = locator;
        }

        public IWebElement ResolveFor(Actor actor)
        {
            return GetDriver(actor).FindElement(_locator);
            //TODO: ensure that actor can see the element
        }
    }
}
