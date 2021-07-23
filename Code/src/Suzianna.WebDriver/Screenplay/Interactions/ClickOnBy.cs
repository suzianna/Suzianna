using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    internal class ClickOnBy : ByAction, IClickInteraction
    {
        public ClickOnBy(By selector) : base(selector) { }
        public override void PerformAs<T>(T actor)
        {
            throw new NotImplementedException();
        }

        public IClickInteraction AfterWaitingUntilEnabled()
        {
            throw new NotImplementedException();
        }

        public IClickInteraction AfterWaitingUntilPresent()
        {
            throw new NotImplementedException();
        }

        public IClickInteraction WithNoDelay()
        {
            throw new NotImplementedException();
        }
    }
}
