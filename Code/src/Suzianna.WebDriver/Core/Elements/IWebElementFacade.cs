using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Suzianna.WebDriver.Core.Elements
{
    public interface IWebElementFacade : IWebElement, IWrapsElement, IWebElementState, ILocatable, IConfigurableTimeouts
    {
        IWebElementFacade Find(By selector);
    }
}
