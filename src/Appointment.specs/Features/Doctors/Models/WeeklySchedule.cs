namespace Appointment.specs.Features.Doctors.Models;

public record WeeklySchedule(int DateOfWeek, Range<TimeOnly>[] DaySchedules);