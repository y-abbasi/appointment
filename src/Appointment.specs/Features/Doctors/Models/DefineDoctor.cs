namespace Appointment.specs.Features.Doctors.Models;

public record DefineDoctor(DoctorSpeciality DoctorSpeciality, WeeklySchedule WeeklySchedule)
{
    public Guid Id { get; init; }
}