namespace Appointment.specs.Features.Doctors.Models;

public record DoctorBuilder(string Name, DoctorSpeciality DoctorSpeciality)
{
    public WeeklySchedule WeeklySchedule { get; init; }
    
    public DefineDoctor Build() => new(DoctorSpeciality, WeeklySchedule);
}