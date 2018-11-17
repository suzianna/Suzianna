using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Suzianna.Web.GUI.Configuration;

namespace Suzianna.Web.GUI
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(SuziannaWebGuiConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            //TODO: Consider refactoring 
            if (IsWebDriverChromeDriver(config))
            {
               var options = CreateChromeWebDriverOptions(config);
               return new ChromeDriver(config.WebDriverPath, options);

            }
            else if(IsWebDriverChromeHeadless(config))
            {
               var options = CreateChromeHeadlessWebDriver(config);
               return new ChromeDriver(config.WebDriverPath, options);
            }
            
            throw  new Exception("Bad request");
        }

        private static ChromeOptions CreateChromeHeadlessWebDriver(SuziannaWebGuiConfig config)
        {
            var options = new ChromeOptions {BinaryLocation = config.WebDriverPath};
            options.AddArgument("--headless");

            return options;
        }

        private static ChromeOptions CreateChromeWebDriverOptions(SuziannaWebGuiConfig config)
        {
            var options = new ChromeOptions {BinaryLocation = config.WebDriverPath};

            return options;
        }

        private static bool IsWebDriverChromeHeadless(SuziannaWebGuiConfig config)
        {
            return config.WebDriver == WebDrivers.HeadlessChrome;
        }

        private static bool IsWebDriverChromeDriver(SuziannaWebGuiConfig config)
        {
            return config.WebDriver == WebDrivers.Chrome;
        }
    }
    
    
}
