using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Screenplay.Abilities;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public abstract class WebInteraction : IInteraction
    {
        public abstract void PerformAs<T>(T actor) where T : Actor;
        public IWebDriver GetDriver(Actor actor)
        {
            var ability = actor.AbilityTo<BrowseTheWeb>();
            return ability.WebDriver;
        }
    }
}
