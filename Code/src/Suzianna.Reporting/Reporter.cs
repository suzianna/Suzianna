using Suzianna.Reporting.Template;
using Suzianna.Reporting.XmlNodes;

namespace Suzianna.Reporting
{
    public class Reporter
    {
        private readonly IClock _clock;
        private readonly Report _report;
        public Reporter():this(new SystemClock())
        {

        }
        public Reporter(IClock clock)
        {
            _clock = clock;
            _report = new Report();
        }

        public void TestSuiteStarted()
        {
            _report.SetStart(_clock.Now());
        }

        public void TestSuiteEnded()
        {
            _report.SetEnd(_clock.Now());
        }

        public void FeatureStarted(string title, string description)
        {
            var feature = new FeatureNode()
            {
                Title = title,
                Description = description,
            };
            _report.Features.Add(feature);
        }

        public string ExportXml()
        {
            return XmlAgent.ToXml(this._report);
        }

        public void ScenarioStarted(string featureTitle, string scenarioTitle)
        {
            _report.StartScenario(featureTitle, scenarioTitle, _clock.Now());
        }

        public void MarkScenarioAsPassed(string featureTitle, string scenarioTitle)
        {
            _report.MarkScenarioAsPassed(featureTitle, scenarioTitle, _clock.Now());
        }

        public void MarkScenarioAsFailed(string featureTitle, string scenarioTitle, string reason = null)
        {
            _report.MarkScenarioAsFailed(featureTitle, scenarioTitle, _clock.Now(), reason);
        }

        public void StepStarted(string featureTitle, string scenarioTitle, string stepText)
        {
            _report.StartStep(featureTitle, scenarioTitle, stepText);
        }

        public void EventPublished(string featureTitle, string scenarioTitle, string eventText)
        {
            _report.EventPublished(featureTitle, scenarioTitle, eventText);
        }
    }
}