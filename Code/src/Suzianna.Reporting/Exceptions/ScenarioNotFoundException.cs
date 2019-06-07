using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Reporting.Exceptions
{
    public class ScenarioNotFoundException : Exception
    {
        public string ScenarioTitle { get;private set; }
        public string FeatureTitle { get;private set; }
        public ScenarioNotFoundException(string scenarioTitle, string featureTitle) 
            : base(string.Format(ExceptionMessages.ScenarioNotFound, scenarioTitle, featureTitle))
        {
            this.ScenarioTitle = scenarioTitle;
            this.FeatureTitle = featureTitle;
        }
    }
}
