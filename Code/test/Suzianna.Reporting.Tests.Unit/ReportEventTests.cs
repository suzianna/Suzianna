using NFluent;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportEventTests : ReportTests
    {
        private Feature _feature;
        private Scenario _scenario;
        
        public ReportEventTests()
        {
            _feature = SampleFeatures.ReturnsGoToStock;
            _scenario = SampleScenarios.RefundedItems;

            Reporter.FeatureStarted(_feature);
            Reporter.ScenarioStarted(_feature.Title, _scenario);
            Reporter.StepStarted(_feature.Title, _scenario.Title, SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(_feature.Title, _scenario.Title, SampleSteps.RefundedItems.WhenText);
        }
        
        [Fact]
        public void should_add_events_latest_step_of_scenario()
        {
            Reporter.EventPublished(_feature.Title, _scenario.Title, SampleEvents.AdminAttemptsToDefineUsers);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/Steps/Step/Event/text()"))
                 .IsEqualTo(SampleEvents.AdminAttemptsToDefineUsers);
        }
    }
}