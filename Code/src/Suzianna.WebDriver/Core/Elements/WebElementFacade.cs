using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.PageObjects;

namespace Suzianna.WebDriver.Core.Elements
{
    public class WebElementFacade : IWebElementFacade
    {
        private readonly IWebDriver _driver;
        private readonly IElementLocator _locator;
        private readonly IWebElement _webElement;
        private readonly By _bySelector;
        public string TagName { get; }
        public string Text { get; }
        public bool Enabled { get; }
        public bool Selected { get; }
        public Point Location { get; }
        public Size Size { get; }
        public bool Displayed { get; }
        public IWebElement WrappedElement { get; }
        public Point LocationOnScreenOnceScrolledIntoView { get; }
        public ICoordinates Coordinates { get; }
        public WebElementFacade(WebElementFacadeBuilder builder)
        {
            this._driver = builder.Driver;
            this._locator = builder.Locator;
            this._webElement = builder.WebElement;
            this._bySelector = builder.BySelector;
        }

        public IWebElement FindElement(By @by)
        {
            throw new NotImplementedException();
        }
        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public void SendKeys(string text)
        {
            throw new NotImplementedException();
        }
        public void Submit()
        {
            throw new NotImplementedException();
        }
        public void Click()
        {
            throw new NotImplementedException();
        }
        public string GetAttribute(string attributeName)
        {
            throw new NotImplementedException();
        }
        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }
        public string GetCssValue(string propertyName)
        {
            throw new NotImplementedException();
        }
        public void SetImplicitTimeout(TimeSpan implicitTimeout)
        {
            throw new NotImplementedException();
        }
        public TimeSpan GetCurrentImplicitTimeout()
        {
            throw new NotImplementedException();
        }
        public TimeSpan ResetTimeouts()
        {
            throw new NotImplementedException();
        }

        public IWebElementFacade Find(By selector)
        {
            return WebElementFacadeBuilder
                .NewInstance()
                .WithDriver(_driver)
                .By(selector)
                .Build();
        }
    }
}