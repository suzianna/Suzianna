using System.Linq;
using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Suzianna.Reporting.XmlNodes;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit.ReportParsingTests
{
    public class ReportParserStepTests : ReportTests
    {
        private readonly Feature _feature;
        private readonly string _scenarioTitle;
        private readonly string _step;
        public ReportParserStepTests()
        {
            _feature = SampleFeatures.ReturnsGoToStock;
            _scenarioTitle = SampleScenarios.RefundedItems;
            _step = SampleSteps.RefundedItems.GivenText;
            
            Reporter.FeatureStarted(_feature.Title, _feature.Description);
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            
        }
        
        [Fact]
        public void should_parse_steps()
        {
            Reporter.StepStarted(_feature.Title, _scenarioTitle, _step);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestStep(report).Text).IsEqualTo(_step);
        }

        [Fact]
        public void should_parse_events_in_steps()
        {
            const string eventText = SampleEvents.AdminAttemptsToDefineUsers;
            Reporter.StepStarted(_feature.Title, _scenarioTitle, _step);
            Reporter.EventPublished(_feature.Title, _scenarioTitle, eventText);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestStep(report).Events).ContainsExactly(eventText);
        }

        private static StepNode LatestStep(Report report)
        {
            return report.Features.First().Scenarios.First().Steps.First();
        }
    }
}