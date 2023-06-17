using System;

namespace Suzianna.Reporting.Exceptions;

public class ScenarioNotFoundException : Exception
{
    public ScenarioNotFoundException(string scenarioTitle, string featureTitle)
        : base(string.Format(ExceptionMessages.ScenarioNotFound, scenarioTitle, featureTitle))
    {
        ScenarioTitle = scenarioTitle;
        FeatureTitle = featureTitle;
    }

    public string ScenarioTitle { get; }
    public string FeatureTitle { get; }
}