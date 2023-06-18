using Appointment.Domain.Doctors;

namespace Appointment.Domain.Tests.Doctors;

public record DoctorBuilder
{
    public static DoctorBuilder Default => new();
    public DoctorSpeciality DoctorSpeciality { get; init; }
}
