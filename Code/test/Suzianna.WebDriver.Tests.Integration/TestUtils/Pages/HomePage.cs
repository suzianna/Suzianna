using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.WebDriver.Pages;
using Suzianna.WebDriver.Screenplay.Questions;

namespace Suzianna.WebDriver.Tests.Integration.TestUtils.Pages
{
    internal class HomePage : BaseTestPage
    {
        public override string Url => GetLocalUrl("index.html");
        private static By TitleElement => By.Id("title");
        public static IQuestion<string> Title()
        {
            return Question.From(actor => Text.Of(TitleElement).ViewedBy(actor).AsString());
        }
    }
}
