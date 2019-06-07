using NFluent;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportScenarioTests : ReportTests
    {
        private readonly Feature _feature = TestConstants.SampleFeatures.ReturnsGoToStock;

        public ReportScenarioTests()
        {
            Reporter.FeatureStarted(_feature);
        }

        [Fact]
        public void should_add_scenario_with_correct_title_to_report()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@Title")).IsEqualTo(scenario.Title);
        }

        [Fact]
        public void should_mark_scenario_as_passed_in_report()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            Reporter.MarkScenarioAsPassed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@Status"))
                .IsEqualTo(ScenarioStatus.Passed);
        }

        [Fact]
        public void should_mark_scenario_as_failed_in_report()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            Reporter.MarkScenarioAsFailed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@Status"))
                .IsEqualTo(ScenarioStatus.Failed);
        }

        [Fact]
        public void should_put_fail_reason_when_scenario_has_a_reason_of_failure()
        {
            var reason = "I don't know, Something bad happened.";
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            Reporter.MarkScenarioAsFailed(_feature.Title, scenario.Title, reason);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/FailureReason/text()"))
                .IsEqualTo(reason);
        }

        [Fact]
        public void should_not_put_failure_reason_when_scenario_has_not_a_reason_of_failure()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            Reporter.MarkScenarioAsFailed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/FailureReason"))
                .IsEqualTo(string.Empty);
        }

        [Fact]
        public void should_set_scenario_start_time()
        {
            var start = DateFactory.SomeDate();
            TimeTravelTo(start);
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@Start"))
                .IsEqualTo(start.ToReportFormat());
        }

        [Fact]
        public void should_set_scenario_end_time_when_scenario_marked_as_passed()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            var end = DateFactory.SomeDate();
            TimeTravelTo(end);
            Reporter.MarkScenarioAsPassed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@End")).IsEqualTo(end.ToReportFormat());
        }

        [Fact]
        public void should_set_scenario_end_time_when_scenario_marked_as_failed()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.ScenarioStarted(_feature.Title, scenario);
            var end = DateFactory.SomeDate();
            TimeTravelTo(end);
            Reporter.MarkScenarioAsFailed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@End")).IsEqualTo(end.ToReportFormat());
        }

        [Fact]
        public void should_set_scenario_duration_based_on_start_and_end_time()
        {
            var scenario = TestConstants.SampleScenarios.ReplacedItems;
            var start = DateFactory.SomeDate().At("5:30:00");
            var end = DateFactory.SomeDate().At("5:32:30");
            var expected = "00:02:30";
            TimeTravelTo(start);
            Reporter.ScenarioStarted(_feature.Title, scenario);
            TimeTravelTo(end);
            Reporter.MarkScenarioAsPassed(_feature.Title, scenario.Title);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/@Duration")).IsEqualTo(expected);
        }
    }
}