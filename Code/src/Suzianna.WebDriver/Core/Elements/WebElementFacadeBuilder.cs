using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Suzianna.WebDriver.Core.Elements
{
    public class WebElementFacadeBuilder
    {
        public IWebDriver Driver { get; private set; }
        public IElementLocator Locator { get; private set; }
        public IWebElement WebElement { get; private set; }
        public By BySelector { get; private set; }
        public static WebElementFacadeBuilder NewInstance()
        {
            return new WebElementFacadeBuilder();
        }
        private long DefaultWaitForTimeout()
        {
            return 0;
        }
        public WebElementFacadeBuilder WithDriver(IWebDriver driver)
        {
            this.Driver = driver;
            return this;
        }

        public WebElementFacadeBuilder LocateWith(IElementLocator locator)
        {
            this.Locator = locator;
            return this;
        }
        public WebElementFacadeBuilder By(By by)
        {
            this.BySelector = by;
            return this;
        }
        public WebElementFacade Build()
        {
            return new WebElementFacade(this);
        }
    }
}
