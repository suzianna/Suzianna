using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public class ClickOnBy : ByAction, IClickInteraction<ClickOnBy>
    {
        private ClickStrategy _clickStrategy = ClickStrategy.WaitUntilPresent;
        public ClickOnBy(params By[] locator) : base(locator)
        {
        }
        public override void PerformAs<T>(T actor)
        {
            throw new NotImplementedException();
        }
        public ClickOnBy AfterWaitingUntilEnabled()
        {
            this._clickStrategy = ClickStrategy.WaitUntilEnabled;
            return this;
        }

        public ClickOnBy AfterWaitingUntilPresent()
        {
            this._clickStrategy = ClickStrategy.WaitUntilPresent;
            return this;
        }

        public ClickOnBy WithNoDelay()
        {
            this._clickStrategy = ClickStrategy.Immediate;
            return this;
        }
    }
}
