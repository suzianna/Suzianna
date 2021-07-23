using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Pages;
using Suzianna.WebDriver.Screenplay.Abilities;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    internal class OpenPage : IInteraction
    {
        private readonly Page _targetPage;
        public OpenPage(Page targetPage)
        {
            _targetPage = targetPage;
        }

        public void PerformAs<T>(T actor) where T : Actor
        {
            var ability = actor.AbilityTo<BrowseTheWeb>();
            ability.WebDriver.Navigate().GoToUrl(_targetPage.Url);
        }
    }
}
