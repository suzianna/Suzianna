using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;

namespace Suzianna.WebDriver.Screenplay.Questions
{
    public abstract class TargetedUserInterfaceState<T> : UserInterfaceState<T>
    {
        private readonly By _selector;
        protected TargetedUserInterfaceState(By selector)
        {
            _selector = selector;
        }

        public override Value<T> ViewedBy(Actor actor)
        {
            var browseTheWeb = actor.AbilityTo<BrowseTheWeb>();
            var element =  browseTheWeb.WebDriver.FindElement(_selector);
            return ResolveValueFromElement(element);
        }
        protected abstract T ResolveValueFromElement(IWebElement element);
    }
}
