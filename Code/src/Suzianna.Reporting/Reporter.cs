using Suzianna.Reporting.Model;

namespace Suzianna.Reporting
{
    public class Reporter
    {
        private readonly IClock _clock;
        private readonly Report _report;

        public Reporter(IClock clock)
        {
            _clock = clock;
            _report = new Report();
        }

        public void TestSuiteStarted()
        {
            _report.TotalDuration.SetStartDate(_clock.Now());
        }

        public void TestSuiteEnded()
        {
            _report.TotalDuration.SetEndDate(_clock.Now());
        }

        public void FeatureStarted(Feature feature)
        {
            _report.AddFeature(feature);
        }

        public string GetReport()
        {
            return _report.ToXml();
        }

        public void ScenarioStarted(string featureTitle, Scenario scenario)
        {
            _report.StartScenario(featureTitle, scenario, _clock.Now());
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
    }
}