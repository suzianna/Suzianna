using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Core.Screenplay;

namespace Suzianna.WebDriver.Screenplay
{
    public interface IClickInteraction : IInteraction
    {
        /// <summary>
        /// Wait until the element is present and enabled before clicking
        /// </summary>
        /// <returns></returns>
        IClickInteraction AfterWaitingUntilEnabled();

        /// <summary>
        /// Wait until the element is present before clicking (default behaviour)
        /// </summary>
        /// <returns></returns>
        IClickInteraction AfterWaitingUntilPresent();

        /// <summary>
        /// Click immediately, do not check whether the element is present first.
        /// </summary>
        /// <returns></returns>
        IClickInteraction WithNoDelay();
    }
}
