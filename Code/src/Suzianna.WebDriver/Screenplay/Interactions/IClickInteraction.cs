using Suzianna.Core.Screenplay;

namespace Suzianna.WebDriver.Screenplay.Interactions
{
    public interface IClickInteraction<T> : IInteraction where T : IClickInteraction<T>
    {
        /// <summary>
        /// Wait until the element is present and enabled before clicking
        /// </summary>
        /// <returns></returns>
        T AfterWaitingUntilEnabled();

        /// <summary>
        /// Wait until the element is present before clicking (default behaviour)
        /// </summary>
        /// <returns></returns>
        T AfterWaitingUntilPresent();

        /// <summary>
        /// Click immediately, do not check whether the element is present first.
        /// </summary>
        /// <returns></returns>
        T WithNoDelay();
    }
}
