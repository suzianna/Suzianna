using OpenQA.Selenium;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public class Click
    {
        public static IClickInteraction On(By selector)
        {
            return new ClickOnBy(selector);
        }
    }
}