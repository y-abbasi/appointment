namespace Appointment.specs.Features.Patients;

public record PatientModel(string Name)
{
    public string Id { get; init; }
}