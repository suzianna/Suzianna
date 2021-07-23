using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay.Actors;

namespace Suzianna.WebDriver.Screenplay.Questions
{
    public class Text : TargetedUserInterfaceState<string>
    {
        private Text(By selector) : base(selector) { }
        protected override string ResolveValueFromElement(IWebElement element) => element.Text;
        public static Text Of(By selector)
        {
            return new Text(selector);
        }
    }
}
