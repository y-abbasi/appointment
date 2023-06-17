using System;

namespace Suzianna.Reporting.Exceptions;

public class StepNotFoundException : Exception
{
    public StepNotFoundException(string scenarioTitle) : base(string.Format(ExceptionMessages.StepNotFound,
        scenarioTitle))
    {
        ScenarioTitle = scenarioTitle;
    }

    public string ScenarioTitle { get; }
}