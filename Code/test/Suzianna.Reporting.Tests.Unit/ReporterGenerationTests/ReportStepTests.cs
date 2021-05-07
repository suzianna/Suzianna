using System;
using NFluent;
using Suzianna.Reporting.Exceptions;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportStepTests : ReportTests
    {
        private string scenario;
        private Feature feature;
        public ReportStepTests()
        {
            feature = TestConstants.SampleFeatures.ReturnsGoToStock;
            scenario = TestConstants.SampleScenarios.RefundedItems;
            Reporter.FeatureStarted(feature.Title, feature.Description);
            Reporter.ScenarioStarted(feature.Title, scenario);
        }
        
        [Fact]
        public void should_add_steps_in_scenario()
        {
            Reporter.StepStarted(feature.Title, scenario, TestConstants.SampleSteps.RefundedItems.GivenText);

            var report = Reporter.ExportXml();
            
            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode/Scenarios/ScenarioNode/Steps/StepNode[1]/Text"))
                .IsEqualTo(TestConstants.SampleSteps.RefundedItems.GivenText);
        }

        [Fact]
        public void should_add_multiple_steps_in_order()
        {
            Reporter.StepStarted(feature.Title, scenario, TestConstants.SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(feature.Title, scenario, TestConstants.SampleSteps.RefundedItems.AndText);
            Reporter.StepStarted(feature.Title, scenario, TestConstants.SampleSteps.RefundedItems.WhenText);
            Reporter.StepStarted(feature.Title, scenario, TestConstants.SampleSteps.RefundedItems.ThenText);

            var report = Reporter.ExportXml();

            Check.That(report.EvaluateXPath("count(//Report/Features/FeatureNode/Scenarios/ScenarioNode/Steps/StepNode)").ToNumber()).IsEqualTo(4);
            Check.That(report.EvaluateXPath("//StepNode[1]/Text")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.GivenText);
            Check.That(report.EvaluateXPath("//StepNode[2]/Text")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.AndText);
            Check.That(report.EvaluateXPath("//StepNode[3]/Text")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.WhenText);
            Check.That(report.EvaluateXPath("//StepNode[4]/Text")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.ThenText);
        }

        [Fact]
        public void should_throw_on_publishing_events_when_scenario_has_no_steps()
        {
            Action publishingEvent = ()=> Reporter.EventPublished(feature.Title, scenario, TestConstants.SampleEvents.AdminAttemptsToDefineUsers);

            Check.ThatCode(publishingEvent).Throws<StepNotFoundException>().WithProperty(a => a.ScenarioTitle, scenario);
        }
    }
}