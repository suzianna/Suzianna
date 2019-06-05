using NFluent;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportStepTests : ReportTests
    {
        private Scenario scenario;
        private Feature feature;
        public ReportStepTests()
        {
            feature = SampleFeatures.ReturnsGoToStock;
            scenario = SampleScenarios.RefundedItems;
            Reporter.FeatureStarted(feature);
            Reporter.ScenarioStarted(feature.Title, scenario);
        }
        
        [Fact]
        public void should_add_steps_in_scenario()
        {
            Reporter.StepStarted(feature.Title, scenario.Title, SampleSteps.RefundedItems.GivenText);

            var report = Reporter.GetReport().ToXmlSource();
            var x = Reporter.GetReport();
            
            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/Steps/Step[1]/text()"))
                .IsEqualTo(SampleSteps.RefundedItems.GivenText);
        }
    }
}