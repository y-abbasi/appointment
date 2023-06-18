using DevArt.Core.Domain;

namespace Appointment.Domain.Doctors;

public record DoctorId(string Value) : IIdentifier
{
    public string PersistenceId => $"Doctor-{Value}";

    public static DoctorId? New() => new DoctorId(Guid.NewGuid().ToString());
}
public enum DoctorSpeciality
{
    GeneralPractitioner, Specialist
}