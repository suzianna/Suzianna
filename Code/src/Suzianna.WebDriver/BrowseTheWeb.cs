using System;
using System.Linq;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay;

namespace Suzianna.WebDriver
{
    public class BrowseTheWeb : IAbility
    {
        public IWebDriver WebDriver { get; }
        private BrowseTheWeb(IWebDriver driver)
        {
            WebDriver = driver;
        }
        public static IAbility With(IWebDriver webDriver)
        {
            return null;
        }

    }
}