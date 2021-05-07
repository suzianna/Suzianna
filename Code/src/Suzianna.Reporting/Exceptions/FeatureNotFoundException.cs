using System;

namespace Suzianna.Reporting.Exceptions
{
    public class FeatureNotFoundException : Exception
    {
        public string FeatureTitle { get; private set; }
        
        public FeatureNotFoundException(string featureTitle)
            : base(string.Format(ExceptionMessages.FeatureNotFound, featureTitle))
        {
            this.FeatureTitle = featureTitle;
        }
    }
}