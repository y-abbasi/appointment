using System.Collections.Immutable;

namespace Appointment.Domain;

public record WeeklySchedule(ImmutableArray<DailySchedule> DailySchedules)
{
    public bool AcceptAppointmentTime(DateTime appointmentTime)
    {
        var dayOfWeek = (int)appointmentTime.DayOfWeek;
        var dailySchedule = DailySchedules.SingleOrDefault(schedule => schedule.DayOfWeek == dayOfWeek);
        if (dailySchedule == null) return false;
        return dailySchedule.DaySchedules.Any(p => p.Contains(appointmentTime.ToTimeOnly()));
    }
}

public record DailySchedule(int DayOfWeek, ImmutableArray<Range<TimeOnly>> DaySchedules);