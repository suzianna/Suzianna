using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Suzianna.WebDriver.Screenplay.Targets
{
    public class TargetBuilder
    {
        private string _targetElementName;

        public TargetBuilder(string targetElementName)
        {
            _targetElementName = targetElementName;
        }
        public Target LocatedById(string id)
        {
            return Located(By.Id(id));
        }
        public Target LocatedByCss(string css)
        {
            return Located(By.CssSelector(css));
        }
        public Target LocatedByXPath(string xpath)
        {
            return Located(By.XPath(xpath));
        }
        public Target Located(By locator)
        {
            return new ByTarget(_targetElementName, locator);
        }
    }
}
