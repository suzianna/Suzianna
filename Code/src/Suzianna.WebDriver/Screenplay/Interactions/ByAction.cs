using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public abstract class ByAction : IInteraction
    {
        protected By Selector { get; private set; }
        protected ByAction(By selector)
        {
            Selector = selector;
        }
        public abstract void PerformAs<T>(T actor) where T : Actor;
    }
}
