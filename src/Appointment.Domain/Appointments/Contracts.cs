using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;

namespace Appointment.Domain.Appointments;

public interface IAppointment : IAggregateRoot<AppointmentId>,
    IDo<SetAppointmentArg, AppointmentId>
{
}

public record SetAppointmentArg(PatientId PatientId, DateTime AppointmentTime, TimeSpan AppointmentDuration);

public record AppointmentId(DateOnly Date, DoctorId DoctorId) : IIdentifier
{
    public string Value => $"{Date}-{DoctorId.Value}";
    public string PersistenceId => $"Appointment-{Value}";
}

public record AppointmentEvent(AppointmentId AggregateId, long Version) : DomainEvent<AppointmentId>(AggregateId,
    "Appointment", Version);

public record AppointmentSetsEvent(AppointmentId AggregateId, PatientId PatientId, DateTime AppointmentTime, TimeSpan AppointmentDuration,
    long Version) : AppointmentEvent(AggregateId, Version);