using DevArt.Core.Domain;

namespace Appointment.Domain.Patiens;

public record PatientId(string Value):IIdentifier
{
    public string PersistenceId => $"Patient-{Value}";

    public static PatientId New() => new(Guid.NewGuid().ToString());
}