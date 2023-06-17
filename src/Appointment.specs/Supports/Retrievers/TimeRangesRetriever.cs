using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Supports.Retrievers;

using Features.Doctors.Models;

public class TimeRangesRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        return propertyType.IsAssignableTo(typeof(Range<TimeOnly>[]));
    }

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        var value = keyValuePair.Value ?? "";

        return value.Split(',')
            .Select(rangeStr => rangeStr.Split('-'))
            .Select(range => new Range<TimeOnly>(TimeOnly.Parse(range[0]), TimeOnly.Parse(range[1])))
            .ToArray();
    }
}