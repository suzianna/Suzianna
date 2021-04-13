using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;

namespace Suzianna.WebDriver.Screenplay.Abilities
{
    /// <summary>
    /// Gives an actor the ability to browse web.
    /// </summary>
    public class BrowseTheWeb : IAbility
    {
        public IWebDriver WebDriver { get; private set; }
        private BrowseTheWeb(IWebDriver driver)
        {
            WebDriver = driver;
        }
        public static BrowseTheWeb With(IWebDriver driver)
        {
            return new BrowseTheWeb(driver);
        }
    }
}
