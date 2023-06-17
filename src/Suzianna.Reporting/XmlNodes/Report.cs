using System;
using System.Collections.Generic;
using System.Linq;
using Suzianna.Reporting.Exceptions;

namespace Suzianna.Reporting.XmlNodes;

public class Report
{
    public Report()
    {
        Features = new List<FeatureNode>();
    }

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan? Duration { get; set; }
    public List<FeatureNode> Features { get; set; }

    internal void SetStart(DateTime date)
    {
        Start = date;
    }

    internal void SetEnd(DateTime date)
    {
        End = date;
        if (Start != null)
            Duration = End - Start;
    }

    internal void StartScenario(string featureTitle, string scenarioTitle, DateTime date)
    {
        var scenario = new ScenarioNode
        {
            Title = scenarioTitle,
            Start = date
        };
        var feature = FindFeature(featureTitle);
        feature.Scenarios.Add(scenario);
    }


    internal void MarkScenarioAsPassed(string featureTitle, string scenarioTitle, DateTime date)
    {
        var feature = FindFeature(featureTitle);
        feature.MarkScenarioAsPassed(scenarioTitle, date);
    }

    internal void MarkScenarioAsFailed(string featureTitle, string scenarioTitle, DateTime date, string reason)
    {
        var feature = FindFeature(featureTitle);
        feature.MarkScenarioAsFailed(scenarioTitle, reason, date);
    }

    internal void StartStep(string featureTitle, string scenarioTitle, string stepText)
    {
        var feature = FindFeature(featureTitle);
        feature.StartStep(scenarioTitle, stepText);
    }

    internal void EventPublished(string featureTitle, string scenarioTitle, string eventText)
    {
        var feature = FindFeature(featureTitle);
        feature.EventPublished(scenarioTitle, eventText);
    }

    private FeatureNode FindFeature(string featureTitle)
    {
        var feature = Features.FirstOrDefault(a => a.Title == featureTitle);
        if (feature == null)
            throw new FeatureNotFoundException(featureTitle);
        return feature;
    }
}