using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Suzianna.Reporting.Template;

namespace Suzianna.Reporting.Model
{
    public class Report
    {
        private readonly ConcurrentQueue<Feature> _features = new ConcurrentQueue<Feature>();

        public Report()
        {
            TotalDuration = new DateRange();
        }

        public DateRange TotalDuration { get; private set; }
        public List<Feature> Features => _features.ToList();

        public void AddFeature(Feature feature)
        {
            _features.Enqueue(feature);
        }


        public void StartScenario(string featureTitle, Scenario scenario, DateTime date)
        {
            var feature = _features.FirstOrDefault(a => a.Title == featureTitle);
            feature.StartScenario(scenario, date);
        }

        public string ToXml()
        {
            return TemplateAgent.Render(this);
        }

        public void MarkScenarioAsPassed(string featureTitle, string scenarioTitle, DateTime date)
        {
            var feature = _features.FirstOrDefault(a => a.Title == featureTitle);
            feature.MarkScenarioAsPassed(scenarioTitle, date);
        }

        public void MarkScenarioAsFailed(string featureTitle, string scenarioTitle, DateTime date, string reason = null)
        {
            var feature = _features.FirstOrDefault(a => a.Title == featureTitle);
            feature.MarkScenarioAsFailed(scenarioTitle, date, reason);
        }

        public void StartStep(string featureTitle, string scenarioTitle, string stepText)
        {
            var feature = _features.FirstOrDefault(a => a.Title == featureTitle);
            feature.AddStepToScenario(scenarioTitle, stepText);
        }
    }
}