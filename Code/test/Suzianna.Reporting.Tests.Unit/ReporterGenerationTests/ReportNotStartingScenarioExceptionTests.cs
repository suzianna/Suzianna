using System;
using NFluent;
using Suzianna.Reporting.Exceptions;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportNotStartingScenarioExceptionTests : ReportTests
    {
        private Feature _feature;
        private Scenario _notStartedScenario;
        public ReportNotStartingScenarioExceptionTests()
        {
            _feature = TestConstants.SampleFeatures.ReturnsGoToStock;
            _notStartedScenario = TestConstants.SampleScenarios.ReplacedItems;
            Reporter.FeatureStarted(_feature);
        }


        [Fact]
        public void should_throw_on_marking_scenario_as_passed_when_scenario_has_not_started()
        {
            Action passingScenario = () => Reporter.MarkScenarioAsPassed(_feature.Title, _notStartedScenario.Title);

            CheckThatMethodThrowsScenarioNotFoundException(passingScenario);
        }

        [Fact]
        public void should_throw_on_marking_scenario_as_failed_when_scenario_has_not_started()
        {
            Action failingScenario = () => Reporter.MarkScenarioAsFailed(_feature.Title, _notStartedScenario.Title);

            CheckThatMethodThrowsScenarioNotFoundException(failingScenario);
        }

        [Fact]
        public void should_throw_on_starting_a_step_when_scenario_has_not_started()
        {
            var step = TestConstants.SampleSteps.RefundedItems.GivenText;

            Action startingStep = () => Reporter.StepStarted(_feature.Title, _notStartedScenario.Title, step);

            CheckThatMethodThrowsScenarioNotFoundException(startingStep);
        }

        [Fact]
        public void should_throw_on_publishing_an_event_when_scenario_has_not_started()
        {
            var eventText = TestConstants.SampleEvents.AdminAttemptsToDefineUsers;

            Action publishingEvent = () => Reporter.EventPublished(_feature.Title, _notStartedScenario.Title, eventText);

            CheckThatMethodThrowsScenarioNotFoundException(publishingEvent);
        }

        private void CheckThatMethodThrowsScenarioNotFoundException(Action passingScenario)
        {
            Check.ThatCode(passingScenario).Throws<ScenarioNotFoundException>()
                .WithProperty(a => a.FeatureTitle, _feature.Title)
                .And.WithProperty(a => a.ScenarioTitle, _notStartedScenario.Title);
        }
    }
}
