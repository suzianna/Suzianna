using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Suzianna.Core.Screenplay.Actors;
using Xunit;
using Xunit.Sdk;

namespace Suzianna.WebDriver.Tests.Integration
{
    [Collection("Chrome Interactions")]
    public class WebInteractionTests
    {
        private ChromeFixture _chromeFixture;
        private IWebDriver ChromeDriver => _chromeFixture.ChromeDriver;
        public WebInteractionTests(ChromeFixture chromeFixture)
        {
            this._chromeFixture = chromeFixture;
        }

        [Fact]
        public void Jack_clicking_on_a_button()
        {
            var jack = Actor.Named("Jack")
                .Can(BrowseTheWeb.With(ChromeDriver));
        }
    }
    [CollectionDefinition("Chrome Interactions")]
    public class DatabaseCollection : ICollectionFixture<ChromeFixture>
    {
    }
    public class ChromeFixture : IDisposable
    {
        public IWebDriver ChromeDriver { get; private set; }

        public ChromeFixture()
        {
            ChromeDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService("D:\\CODE\\Work\\Suzianna\\Code\\src\\Suzianna.WebDriver\\bin\\Debug\\netstandard2.0"));

        }

        public void Dispose()
        {
            ChromeDriver.Close();
            ChromeDriver.Dispose();
        }

    }
}

