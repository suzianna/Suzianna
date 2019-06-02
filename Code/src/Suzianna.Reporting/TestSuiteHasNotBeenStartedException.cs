using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Reporting
{
    public class TestSuiteHasNotBeenStartedException : Exception
    {
        public TestSuiteHasNotBeenStartedException() : 
            base($"Test Suite has not been started started yet. use {nameof(Reporter.TestSuiteStarted)} to start the test suite.")
        {
            
        }
    }
}
