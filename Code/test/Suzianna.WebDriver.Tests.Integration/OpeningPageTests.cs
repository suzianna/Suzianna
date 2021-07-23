using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NFluent;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Suzianna.Core.Functional;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.WebDriver.Screenplay.Abilities;
using Suzianna.WebDriver.Screenplay.Interactions;
using Suzianna.WebDriver.Screenplay.Questions;
using Suzianna.WebDriver.Tests.Integration.TestUtils.Pages;
using Xunit;

namespace Suzianna.WebDriver.Tests.Integration
{
    public class OpeningPageTests : DriverIntegrationTest
    {
        [Fact]
        public void can_open_a_page()
        {
            var actor = Actor.Named("jack").WhoCan(BrowseTheWeb.With(Driver));

            actor.AttemptsTo(Open.The(Home));

            actor.Should(See.That(HomePage.Title())).IsEqualTo("The Screenplay Demo");
        }
    }
}
