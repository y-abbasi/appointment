namespace Appointment.specs.Features.Doctors.Models;

public record DoctorModel(string Name, DoctorSpeciality DoctorSpeciality)
{
    public Guid Id { get; init; }
}