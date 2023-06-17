using System;

namespace Suzianna.Reporting;

public class SystemClock : IClock
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}