using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Suzianna.WebDriver.Tests.Integration.TestUtils.Pages;
using Xunit;

namespace Suzianna.WebDriver.Tests.Integration
{
    public abstract class DriverIntegrationTest : IDisposable
    {
        public IWebDriver Driver { get; private set; }
        internal HomePage Home { get; private set; }
        protected DriverIntegrationTest()
        {
            Driver = new ChromeDriver();
            Home = new HomePage();
        }

        public void Dispose()
        {
            Driver.Close();
            Driver?.Dispose();
        }
    }
}
