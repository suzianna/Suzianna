using System;
using System.Linq;
using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Suzianna.Reporting.XmlNodes;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit.ReportParsingTests
{
    public class ReportParserScenarioTests : ReportTests
    {
        private Feature _feature;
        private string _scenarioTitle;
        public ReportParserScenarioTests()
        {
            _feature = SampleFeatures.ReturnsGoToStock;
            _scenarioTitle = SampleScenarios.RefundedItems;
            Reporter.FeatureStarted(_feature.Title, _feature.Description);
        }
        
        [Fact]
        public void should_parse_scenario()
        {
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.Features.First().Scenarios).CountIs(1);
            Check.That(LatestScenario(report).Title).IsEqualTo(_scenarioTitle);
        }

    

        [Fact]
        public void should_parse_scenario_start_time()
        {
            var start = DateTime.Parse("2010/01/02 15:00:00");
            TimeTravelTo(start);
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).Start).IsEqualTo(start);
        }
        
        [Fact]
        public void should_parse_scenario_end_time()
        {
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            var end = DateTime.Parse("2010/01/02 16:00:00");
            TimeTravelTo(end);
            Reporter.MarkScenarioAsPassed(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).End).IsEqualTo(end);
        }

        [Fact]
        public void should_parse_scenario_duration()
        {
            var start = DateTime.Parse("2010/01/02 15:50:00");
            var end = DateTime.Parse("2010/01/02 16:00:00");
            var expected = TimeSpan.Parse("00:10:00");
            TimeTravelTo(start);
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            TimeTravelTo(end);
            Reporter.MarkScenarioAsPassed(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).Duration).IsEqualTo(expected);
        }
        
        [Fact]
        public void should_parse_scenario_as_null_when_scenario_is_not_ended()
        {
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).Duration).IsNull();
        }
        
        [Fact]
        public void should_parse_scenario_status_when_scenario_is_passed()
        {
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            Reporter.MarkScenarioAsPassed(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).Status).IsEqualTo(ScenarioStatus.Passed);
        }
        
        [Fact]
        public void should_parse_scenario_status_when_scenario_is_failed()
        {
            Reporter.ScenarioStarted(_feature.Title, _scenarioTitle);
            Reporter.MarkScenarioAsFailed(_feature.Title, _scenarioTitle);
            
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(LatestScenario(report).Status).IsEqualTo(ScenarioStatus.Failed);
        }
        
        private static ScenarioNode LatestScenario(Report report)
        {
            return report.Features.First().Scenarios.First();
        }
    }
}