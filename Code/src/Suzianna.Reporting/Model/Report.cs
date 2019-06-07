using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Suzianna.Reporting.Exceptions;
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

        internal void AddFeature(Feature feature)
        {
            _features.Enqueue(feature);
        }
        internal void StartScenario(string featureTitle, Scenario scenario, DateTime date)
        {
            var feature = FindFeature(featureTitle);
            feature.StartScenario(scenario, date);
        }
        internal void MarkScenarioAsPassed(string featureTitle, string scenarioTitle, DateTime date)
        {
            var feature = FindFeature(featureTitle);
            feature.MarkScenarioAsPassed(scenarioTitle, date);
        }

        internal void MarkScenarioAsFailed(string featureTitle, string scenarioTitle, DateTime date, string reason = null)
        {
            var feature = FindFeature(featureTitle);
            feature.MarkScenarioAsFailed(scenarioTitle, date, reason);
        }

        internal void StartStep(string featureTitle, string scenarioTitle, string stepText)
        {
            var feature = FindFeature(featureTitle);
            feature.AddStepToScenario(scenarioTitle, stepText);
        }

        internal void EventPublished(string featureTitle, string scenarioTitle, string eventText)
        {
            var feature = FindFeature(featureTitle);
            feature.AddEventTheToLatestStepOf(scenarioTitle, eventText);
        }
        
        private Feature FindFeature(string featureTitle)
        {
            var feature = _features.FirstOrDefault(a => a.Title == featureTitle);
            if (feature == null) 
                throw new FeatureNotFoundException(featureTitle);
            return feature;
        }
    }
}