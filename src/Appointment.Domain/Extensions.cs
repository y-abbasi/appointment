namespace Appointment.Domain;

public static class DateTimeExtension
{
    public static DateOnly ToDateOnly(this DateTime d)
    {
        return new DateOnly(d.Year, d.Month, d.Day);
    }

    public static TimeOnly ToTimeOnly(this DateTime d)
    {
        return new TimeOnly(d.Hour, d.Minute, d.Second, d.Millisecond);
    }
}