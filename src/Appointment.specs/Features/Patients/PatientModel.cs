namespace Appointment.specs.Features.Patients;

public record PatientModel(string Name)
{
    public Guid Id { get; init; }
}