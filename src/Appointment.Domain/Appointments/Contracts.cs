using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;

namespace Appointment.Domain.Appointments;

public interface IAppointment : IAggregateRoot<IAppointmentState, AppointmentId>,
    IDo<SetAppointmentArg, AppointmentId>
{
}

public record SetAppointmentArg(PatientId PatientId, DateTime AppointmentTime, TimeSpan AppointmentDuration,
    IDoctorService DoctorService);

public record AppointmentId(DateOnly Date, DoctorId DoctorId) : IIdentifier
{
    public string Value => $"{Date}-{DoctorId.Value}";
    public string PersistenceId => $"Appointment-{Value}";
}

public record AppointmentEvent(AppointmentId AggregateId, long Version) : DomainEvent<AppointmentId>(AggregateId,
    "Appointment", Version);

public record AppointmentSetsEvent(AppointmentId AggregateId, PatientId PatientId, DateTime AppointmentTime,
    TimeSpan AppointmentDuration,
    long Version) : AppointmentEvent(AggregateId, Version);

public class AppointmentExceptionCodes
{
    public const string MustBeWithinWorkingHourOfClinic = "BR-AP-100";
    public const string MustBeAppropriateToTheDoctorSpeciality = "BR-AP-101";
    public const string MustBeADuringTheDoctorsPresents = "BR-AP-102";
}