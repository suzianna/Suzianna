using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.WebDriver.Core.Elements;

namespace Suzianna.WebDriver.Screenplay.Abilities
{
    /// <summary>
    /// Gives an actor the ability to browse web.
    /// </summary>
    public class BrowseTheWeb : IAbility, IReferToActor
    {
        public IWebDriver WebDriver { get; private set; }
        public Actor Actor { get; private set; }
        private BrowseTheWeb(IWebDriver driver)
        {
            WebDriver = driver;
        }
        public static BrowseTheWeb With(IWebDriver driver)
        {
            return new BrowseTheWeb(driver);
        }
        public static BrowseTheWeb As(Actor actor)
        {
            var browseTheWeb = actor.AbilityTo<BrowseTheWeb>();
            return browseTheWeb.AsActor<BrowseTheWeb>(actor);
        }
        public T AsActor<T>(Actor actor) where T : class, IAbility
        {
            this.Actor = actor;
            return this as T;
        }


        //----------------------- Page object abstraction
        public IWebElementFacade Find(By selector)
        {
            //TODO: pass 'ImplicitTimeoutInMilliseconds' & 'WaitForTimeoutInMilliseconds' from config to this builder
            return WebElementFacadeBuilder
                .NewInstance()
                .WithDriver(WebDriver)
                .By(selector)
                .Build();
        }
    }
}
