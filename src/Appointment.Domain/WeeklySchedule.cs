using System.Collections.Immutable;

namespace Appointment.Domain;

public record WeeklySchedule(ImmutableArray<DailySchedule> DailySchedules);
public record DailySchedule(int DateOfWeek, ImmutableArray<Range<TimeOnly>> DaySchedules);