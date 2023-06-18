using System.Collections.Immutable;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;

namespace Appointment.Domain.Appointments;

public interface IAppointment : IAggregateRoot<IAppointmentState, AppointmentId>,
    IDo<SetAppointmentArg, AppointmentId>
{
}

public record AppointmentEntity(DoctorId DoctorId, PatientId PatientId, DateTime AppointmentTime,
    TimeSpan AppointmentDuration);

public interface IAppointmentService
{
    Task<ImmutableArray<AppointmentEntity>> GetPatientAppointmentsInDay(PatientId patientId,
        DateOnly appointmentDate);
}

public record SetAppointmentArg(PatientId PatientId, DateTime AppointmentTime, TimeSpan AppointmentDuration,
    IAppointmentService AppointmentService, IDoctorService DoctorService);

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
    public const string PatientMustBeLessThanTwoAppointmentAtTheSameDay = "BR-AP-103";
    public const string AppointmentsOfPatientShouldNotOverlap = "BR-AP-104";
    public const string TheNumberOfDoctorsOverlappingAppointmentsShouldNotExceededTheAllowedNumberOfOverlappingAppointments = "BR-AP-105";
}