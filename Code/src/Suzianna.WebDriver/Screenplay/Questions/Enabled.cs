using OpenQA.Selenium;

namespace Suzianna.WebDriver.Screenplay.Questions
{
    public class Enabled : TargetedUserInterfaceState<bool>
    {
        private Enabled(By selector) : base(selector) { }
        protected override bool ResolveValueFromElement(IWebElement element) => element.Enabled;
        public static Enabled Of(By selector)
        {
            return new Enabled(selector);
        }
    }
}