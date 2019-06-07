using System;
using NFluent;
using Suzianna.Reporting.Exceptions;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportNotStartingFeatureExceptionTests : ReportTests
    {
        private readonly string _nonStartedFeature;
        public ReportNotStartingFeatureExceptionTests()
        {
            _nonStartedFeature = TestConstants.SampleFeatures.ReturnsGoToStock.Title;
        }
        
        [Fact]
        public void should_throw_on_adding_a_scenario_to_a_non_started_feature()
        {
            var scenario = TestConstants.SampleScenarios.RefundedItems;
            
            Action startingScenario = ()=> Reporter.ScenarioStarted(_nonStartedFeature, scenario);

            CheckThatMethodThrowsFeatureNotFoundException(startingScenario);
        }

        [Fact]
        public void should_throw_on_marking_a_scenario_as_passed_in_a_non_started_feature()
        {
            var scenario = TestConstants.SampleScenarios.RefundedItems;

            Action markScenarioAsPassed = () => Reporter.MarkScenarioAsPassed(_nonStartedFeature, scenario);

            CheckThatMethodThrowsFeatureNotFoundException(markScenarioAsPassed);
        }
        
        [Fact]
        public void should_throw_on_marking_a_scenario_as_failed_in_a_non_started_feature()
        {
            var scenario = TestConstants.SampleScenarios.RefundedItems;

            Action markScenarioAsPassed = () => Reporter.MarkScenarioAsFailed(_nonStartedFeature, scenario);

            CheckThatMethodThrowsFeatureNotFoundException(markScenarioAsPassed);
        }

        [Fact]
        public void should_throw_on_starting_a_step_in_a_non_started_feature()
        {
            var scenario = TestConstants.SampleScenarios.RefundedItems;
            var step = TestConstants.SampleSteps.RefundedItems.GivenText;

            Action startingStep = () => Reporter.StepStarted(_nonStartedFeature, scenario, step);
            
            CheckThatMethodThrowsFeatureNotFoundException(startingStep);
        }

        [Fact]
        public void should_throw_on_publishing_an_event_in_a_non_started_feature()
        {
            var scenario = TestConstants.SampleScenarios.RefundedItems;
            var eventText = TestConstants.SampleEvents.AdminAttemptsToDefineUsers;

            Action publishingEvent = () => Reporter.EventPublished(_nonStartedFeature, scenario, eventText);
            
            CheckThatMethodThrowsFeatureNotFoundException(publishingEvent);
        }

        private void CheckThatMethodThrowsFeatureNotFoundException(Action startingStep)
        {
            Check.ThatCode(startingStep).Throws<FeatureNotFoundException>()
                .WithProperty(a => a.FeatureTitle, _nonStartedFeature);
        }
    }
}