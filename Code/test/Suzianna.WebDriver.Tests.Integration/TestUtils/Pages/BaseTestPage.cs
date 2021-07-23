using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suzianna.WebDriver.Pages;

namespace Suzianna.WebDriver.Tests.Integration.TestUtils.Pages
{
    internal abstract class BaseTestPage : Page
    {
        protected string GetLocalUrl(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Sample-Web-Site\{filename}");
        }
    }
}
