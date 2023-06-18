using System.Collections.Immutable;

namespace Appointment.specs.Features.Doctors.Models;

public record WeeklySchedule(ImmutableArray<DailySchedule> DailySchedules);
public record DailySchedule(DayOfWeek DayOfWeek, Range<string>[] DaySchedules);