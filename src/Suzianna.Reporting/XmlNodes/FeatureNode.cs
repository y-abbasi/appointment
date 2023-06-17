using System;
using System.Collections.Generic;
using System.Linq;
using Suzianna.Reporting.Exceptions;

namespace Suzianna.Reporting.XmlNodes;

public class FeatureNode
{
    public FeatureNode()
    {
        Scenarios = new List<ScenarioNode>();
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public List<ScenarioNode> Scenarios { get; set; }

    internal void MarkScenarioAsPassed(string scenarioTitle, DateTime date)
    {
        var scenario = FindScenario(scenarioTitle);
        scenario.MarkAsPassed(date);
    }


    internal void MarkScenarioAsFailed(string scenarioTitle, string reason, DateTime date)
    {
        var scenario = FindScenario(scenarioTitle);
        scenario.MarkAsFailed(reason, date);
    }

    internal void StartStep(string scenarioTitle, string stepText)
    {
        var scenario = FindScenario(scenarioTitle);
        scenario.Steps.Add(new StepNode
        {
            Text = stepText
        });
    }

    internal void EventPublished(string scenarioTitle, string eventText)
    {
        var scenario = FindScenario(scenarioTitle);

        AddEventToLatestStep(scenarioTitle, eventText, scenario);
    }

    private static void AddEventToLatestStep(string scenarioTitle, string eventText, ScenarioNode scenario)
    {
        if (scenario.Steps.IsEmpty())
            throw new StepNotFoundException(scenarioTitle);
        var lastStep = scenario.Steps.Last();
        lastStep.Events.Add(eventText);
    }

    private ScenarioNode FindScenario(string scenarioTitle)
    {
        var scenario = Scenarios.FirstOrDefault(a => a.Title == scenarioTitle);
        if (scenario == null)
            throw new ScenarioNotFoundException(scenarioTitle, Title);
        return scenario;
    }
}