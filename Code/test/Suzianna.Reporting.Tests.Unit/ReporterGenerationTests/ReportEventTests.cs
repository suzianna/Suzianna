using NFluent;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportEventTests : ReportTests
    {
        private readonly Feature _feature;
        private readonly Scenario _scenario;
        
        public ReportEventTests()
        {
            _feature = TestConstants.SampleFeatures.ReturnsGoToStock;
            _scenario = TestConstants.SampleScenarios.RefundedItems;

            Reporter.FeatureStarted(_feature);
            Reporter.ScenarioStarted(_feature.Title, _scenario);
            Reporter.StepStarted(_feature.Title, _scenario.Title, TestConstants.SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(_feature.Title, _scenario.Title, TestConstants.SampleSteps.RefundedItems.WhenText);
        }
        
        [Fact]
        public void should_add_events_latest_step_of_scenario()
        {
            Reporter.EventPublished(_feature.Title, _scenario.Title, TestConstants.SampleEvents.AdminAttemptsToDefineUsers);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/Steps/Step/Event/text()"))
                 .IsEqualTo(TestConstants.SampleEvents.AdminAttemptsToDefineUsers);
        }
    }
}