using System.Collections.Generic;

namespace Suzianna.Reporting.XmlNodes;

public class StepNode
{
    public StepNode()
    {
        Events = new List<string>();
    }

    public string Text { get; set; }
    public List<string> Events { get; set; }
}