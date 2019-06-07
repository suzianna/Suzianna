using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Reporting.Exceptions
{
    public class StepNotFoundException : Exception
    {
        public string ScenarioTitle { get; private set; }

        public StepNotFoundException(string scenarioTitle) : base(string.Format(ExceptionMessages.StepNotFound, scenarioTitle))
        {
            ScenarioTitle = scenarioTitle;
        }
    }
}
