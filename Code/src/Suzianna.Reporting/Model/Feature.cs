using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Suzianna.Reporting.Model
{
    public class Feature
    {
        private readonly ConcurrentQueue<Scenario> _scenarios = new ConcurrentQueue<Scenario>();
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Scenario> Scenarios => _scenarios.ToList();
        
        public void MarkScenarioAsPassed(string scenarioTitle, DateTime date)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.Status = ScenarioStatus.Passed;
            scenario.Duration.SetEndDate(date);
        }

        public void MarkScenarioAsFailed(string scenarioTitle, DateTime date,string reason)
        {
            var scenario = FindScenario(scenarioTitle);
            scenario.Status = ScenarioStatus.Failed;
            scenario.FailureReason = reason;
            scenario.Duration.SetEndDate(date);
        }
        public void StartScenario(Scenario scenario, DateTime date)
        {
            _scenarios.Enqueue(scenario);
            scenario.Duration.SetStartDate(date);
        }
        private Scenario FindScenario(string scenarioTitle)
        {
            return _scenarios.First(a => a.Title == scenarioTitle);
        }

       
    }
}