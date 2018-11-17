using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenQA.Selenium;

namespace Suzianna.Web.GUI
{
    public static class WebDriver
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get => _driver;
            set
            {
                Debug.Assert(_driver == null);
                _driver = value;
            }
        }
    }
}
