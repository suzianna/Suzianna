using System;
using NFluent;
using Suzianna.Reporting.Exceptions;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportStepTests : ReportTests
    {
        private Scenario scenario;
        private Feature feature;
        public ReportStepTests()
        {
            feature = TestConstants.SampleFeatures.ReturnsGoToStock;
            scenario = TestConstants.SampleScenarios.RefundedItems;
            Reporter.FeatureStarted(feature);
            Reporter.ScenarioStarted(feature.Title, scenario);
        }
        
        [Fact]
        public void should_add_steps_in_scenario()
        {
            Reporter.StepStarted(feature.Title, scenario.Title, TestConstants.SampleSteps.RefundedItems.GivenText);

            var report = Reporter.GetReport().ToXmlSource();
            
            Check.That(report.EvaluateXPath("//Report/Features/Feature/Scenario/Steps/Step[1]/text()"))
                .IsEqualTo(TestConstants.SampleSteps.RefundedItems.GivenText);
        }

        [Fact]
        public void should_add_multiple_steps_in_order()
        {
            Reporter.StepStarted(feature.Title, scenario.Title, TestConstants.SampleSteps.RefundedItems.GivenText);
            Reporter.StepStarted(feature.Title, scenario.Title, TestConstants.SampleSteps.RefundedItems.AndText);
            Reporter.StepStarted(feature.Title, scenario.Title, TestConstants.SampleSteps.RefundedItems.WhenText);
            Reporter.StepStarted(feature.Title, scenario.Title, TestConstants.SampleSteps.RefundedItems.ThenText);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("count(//Report/Features/Feature/Scenario/Steps/Step)").ToNumber()).IsEqualTo(4);
            Check.That(report.EvaluateXPath("//Step[1]/text()")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.GivenText);
            Check.That(report.EvaluateXPath("//Step[2]/text()")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.AndText);
            Check.That(report.EvaluateXPath("//Step[3]/text()")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.WhenText);
            Check.That(report.EvaluateXPath("//Step[4]/text()")).IsEqualTo(TestConstants.SampleSteps.RefundedItems.ThenText);
        }

        [Fact]
        public void should_throw_on_publishing_events_when_scenario_has_no_steps()
        {
            Action publishingEvent = ()=> Reporter.EventPublished(feature.Title, scenario.Title, TestConstants.SampleEvents.AdminAttemptsToDefineUsers);

            Check.ThatCode(publishingEvent).Throws<StepNotFoundException>().WithProperty(a => a.ScenarioTitle, scenario.Title);
        }
    }
}