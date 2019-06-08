using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportEventTests : ReportTests
    {
        private readonly Feature _feature;
        private readonly string _scenario;
        
        public ReportEventTests()
        {
            _feature = SampleFeatures.ReturnsGoToStock;
            _scenario = SampleScenarios.RefundedItems;

            Reporter.FeatureStarted(_feature.Title, _feature.Description);
            Reporter.ScenarioStarted(_feature.Title, _scenario);
            Reporter.StepStarted(_feature.Title, _scenario, SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(_feature.Title, _scenario, SampleSteps.RefundedItems.WhenText);
        }
        
        [Fact]
        public void should_add_events_latest_step_of_scenario()
        {
            Reporter.EventPublished(_feature.Title, _scenario, SampleEvents.AdminAttemptsToDefineUsers);

            var report = Reporter.ExportXml().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode/Scenarios/ScenarioNode/Steps/StepNode/Events/string"))
                 .IsEqualTo(TestConstants.SampleEvents.AdminAttemptsToDefineUsers);
        }
    }
}