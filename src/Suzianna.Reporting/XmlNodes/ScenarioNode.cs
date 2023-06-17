using System;
using System.Collections.Generic;

namespace Suzianna.Reporting.XmlNodes;

public class ScenarioNode
{
    public ScenarioNode()
    {
        Steps = new List<StepNode>();
    }

    public string Title { get; set; }
    public ScenarioStatus Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan? Duration { get; set; }
    public string FailureReason { get; set; }
    public List<StepNode> Steps { get; set; }

    internal void MarkAsPassed(DateTime date)
    {
        Status = ScenarioStatus.Passed;
        SetEnd(date);
    }

    internal void MarkAsFailed(string reason, DateTime date)
    {
        Status = ScenarioStatus.Failed;
        FailureReason = reason;
        SetEnd(date);
    }

    private void SetEnd(DateTime date)
    {
        End = date;
        if (Start != null)
            Duration = End - Start;
    }
}