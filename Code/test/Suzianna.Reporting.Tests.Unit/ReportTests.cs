using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Reporting.Tests.Unit.TestDoubles;

namespace Suzianna.Reporting.Tests.Unit
{
    public abstract class ReportTests
    {
        protected readonly Reporter Reporter;
        protected readonly StubClock Clock;
        protected ReportTests()
        {
            Clock = new StubClock();
            Reporter = new Reporter(Clock);
        }

        protected void TimeTravelTo(DateTime time)
        {
            Clock.TimeTravelTo(time);
        }
        protected void GotoTimeAndStartTestSuite(DateTime time)
        {
            TimeTravelTo(time);
            Reporter.TestSuiteStarted();
        }
        protected void GotoTimeAndEndTestSuite(DateTime time)
        {
            TimeTravelTo(time);
            Reporter.TestSuiteEnded();
        }
    }
}
