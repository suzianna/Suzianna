using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Suzianna.Reporting.Exceptions;

namespace Suzianna.Reporting.Model
{
    public class Feature
    {
        private readonly ConcurrentQueue<Scenario> _scenarios = new ConcurrentQueue<Scenario>();
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Scenario> Scenarios => _scenarios.ToList();

        internal void MarkScenarioAsPassed(string scenarioTitle, DateTime date)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.Status = ScenarioStatus.Passed;
            scenario.Duration.SetEndDate(date);
        }

        internal void MarkScenarioAsFailed(string scenarioTitle, DateTime date,string reason)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.Status = ScenarioStatus.Failed;
            scenario.FailureReason = reason;
            scenario.Duration.SetEndDate(date);
        }
        internal void StartScenario(Scenario scenario, DateTime date)
        {
            _scenarios.Enqueue(scenario);
            scenario.Duration.SetStartDate(date);
        }
        internal void AddStepToScenario(string scenarioTitle, string stepText)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.AddStep(new Step(stepText));
        }
        internal void AddEventTheToLatestStepOf(string scenarioTitle, string eventText)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.AddEvent(eventText);
        }
        private Scenario FindScenario(string scenarioTitle)
        {
            var scenario =  _scenarios.FirstOrDefault(a => a.Title == scenarioTitle);
            if (scenario == null)
                throw new ScenarioNotFoundException(scenarioTitle, this.Title);
            return scenario;
        }
    }
}