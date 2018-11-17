using System;
using System.ComponentModel;
using TechTalk.SpecFlow;

namespace Suzianna.SpecflowPlugin
{
    [Binding]
    public static class WebDriverHooks
    {
        [BeforeScenario]
        public static void SetupWebDriver()
        {

        }

        [AfterScenario]
        public static void TeardownWebDriver()
        {

        }
    }
}
