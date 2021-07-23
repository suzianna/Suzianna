using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay;
using Suzianna.WebDriver.Pages;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public static class Open
    {
        public static IInteraction The(Page page)
        {
            return new OpenPage(page);
        }
    }
}
