using System;

namespace Suzianna.Reporting;

public static class DateTimeExtensions
{
    public static string ToReportFormat(this DateTime date)
    {
        return date.ToString(ReportConstants.DateFormat);
    }
}