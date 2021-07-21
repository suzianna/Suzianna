using System;

namespace Suzianna.WebDriver.Core.Elements
{
    public interface IConfigurableTimeouts
    {

        void SetImplicitTimeout(TimeSpan implicitTimeout);
        TimeSpan GetCurrentImplicitTimeout();
        TimeSpan ResetTimeouts();
    }
}