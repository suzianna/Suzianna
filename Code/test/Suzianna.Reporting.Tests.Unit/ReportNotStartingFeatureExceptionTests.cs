using System;
using NFluent;
using Suzianna.Reporting.Exceptions;
using Suzianna.Reporting.Model;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportNotStartingFeatureExceptionTests : ReportTests
    {
        private readonly Feature _nonStartedFeature;

        public ReportNotStartingFeatureExceptionTests()
        {
            _nonStartedFeature = SampleFeatures.ReturnsGoToStock;
        }
        
        [Fact]
        public void should_throw_on_adding_a_scenario_to_a_non_started_feature()
        {
            var scenario = SampleScenarios.RefundedItems;
            
            Action startingScenario = ()=> Reporter.ScenarioStarted(_nonStartedFeature.Title, scenario);

            CheckThatMethodThrowsFeatureNotFoundException(startingScenario);
        }

        [Fact]
        public void should_throw_on_marking_a_scenario_as_passed_in_a_non_started_feature()
        {
            var scenario = SampleScenarios.RefundedItems;

            Action markScenarioAsPassed = () => Reporter.MarkScenarioAsPassed(_nonStartedFeature.Title, scenario.Title);

            CheckThatMethodThrowsFeatureNotFoundException(markScenarioAsPassed);
        }
        
        [Fact]
        public void should_throw_on_marking_a_scenario_as_failed_in_a_non_started_feature()
        {
            var scenario = SampleScenarios.RefundedItems;

            Action markScenarioAsPassed = () => Reporter.MarkScenarioAsFailed(_nonStartedFeature.Title, scenario.Title);

            CheckThatMethodThrowsFeatureNotFoundException(markScenarioAsPassed);
        }

        [Fact]
        public void should_throw_on_starting_a_step_in_a_non_started_feature()
        {
            var scenario = SampleScenarios.RefundedItems;
            var step = SampleSteps.RefundedItems.GivenText;

            Action startingStep = () => Reporter.StepStarted(_nonStartedFeature.Title, scenario.Title, step);
            
            CheckThatMethodThrowsFeatureNotFoundException(startingStep);
        }

        [Fact]
        public void should_throw_on_publishing_an_event_in_a_non_started_feature()
        {
            var scenario = SampleScenarios.RefundedItems;
            var eventText = SampleEvents.AdminAttemptsToDefineUsers;

            Action publishingEvent = () => Reporter.EventPublished(_nonStartedFeature.Title, scenario.Title, eventText);
            
            CheckThatMethodThrowsFeatureNotFoundException(publishingEvent);
        }

        private void CheckThatMethodThrowsFeatureNotFoundException(Action startingStep)
        {
            Check.ThatCode(startingStep).Throws<FeatureNotFoundException>()
                .WithProperty(a => a.FeatureTitle, _nonStartedFeature.Title);
        }
    }
}