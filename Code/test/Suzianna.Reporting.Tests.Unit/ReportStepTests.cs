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
            
            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/Steps/Step[1]/text()"))
                .IsEqualTo(SampleSteps.RefundedItems.GivenText);
        }

        [Fact]
        public void should_add_multiple_steps_in_order()
        {
            Reporter.StepStarted(feature.Title, scenario.Title, SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(feature.Title, scenario.Title, SampleSteps.RefundedItems.AndText);
            Reporter.StepStarted(feature.Title, scenario.Title, SampleSteps.RefundedItems.WhenText);
            Reporter.StepStarted(feature.Title, scenario.Title, SampleSteps.RefundedItems.ThenText);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("count(//Report/Features/Feature/Scenario/Steps/Step)").ToNumber()).IsEqualTo(4);
            Check.That(report.EvaluateXPath("//Step[1]/text()")).IsEqualTo(SampleSteps.RefundedItems.GivenText);
            Check.That(report.EvaluateXPath("//Step[2]/text()")).IsEqualTo(SampleSteps.RefundedItems.AndText);
            Check.That(report.EvaluateXPath("//Step[3]/text()")).IsEqualTo(SampleSteps.RefundedItems.WhenText);
            Check.That(report.EvaluateXPath("//Step[4]/text()")).IsEqualTo(SampleSteps.RefundedItems.ThenText);
        }
    }
}