using System.Collections.Immutable;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Appointments;

public partial class Appointment : AggregateRoot<IAppointmentState, AppointmentId>, IAppointment
{
    public Appointment(AppointmentId id) : base(id, new AppointmentInitializedState(id, 0))
    {
    }

    public override IDomainEvent<AppointmentId> Process(IDomainEvent<AppointmentId> @event)
    {
        dynamic evt = @event;
        AggregateState = AggregateState.Apply(evt);
        return evt;
    }

    public override Task<IEnumerable<IDomainEvent<AppointmentId>>> Do(object arg)
    {
        dynamic msg = arg;
        return Do(msg);
    }

    public async Task<IEnumerable<IDomainEvent<AppointmentId>>> Do(SetAppointmentArg arg)
    {
        GuardAgainstAppointmentTimeOutOfTheClinicWorkingHours(arg.AppointmentTime);
        var doctor = await arg.DoctorService.GetById(Id.DoctorId);
        GuardAgainstAppointmentDurationNotAppropriateToTheDoctorSpeciality(doctor,
            arg.AppointmentDuration);
        GuardAgainstAppointmentTimeDuringDoctorNotPresents(doctor, arg.AppointmentTime);
        var patientsAppointments =
            await arg.AppointmentService.GetPatientAppointmentsInDay(arg.PatientId, arg.AppointmentTime.ToDateOnly());
        GuardAgainstAppointmentsOfPatientHasNotOverlap(patientsAppointments, arg.AppointmentTime,
            arg.AppointmentDuration);
        GuardAgainstPatientMoreThanTwoAppointmentAtTheSameDate(patientsAppointments);
        GuardAgainstInvalidOverlappingAppointment(doctor, arg.AppointmentTime, arg.AppointmentDuration);
        return new List<IDomainEvent<AppointmentId>>()
        {
            Process(new AppointmentSetsEvent(Id, arg.PatientId, arg.AppointmentTime, arg.AppointmentDuration,
                AggregateState.Version + 1))
        };
    }

    #region invariants

    private void GuardAgainstAppointmentTimeOutOfTheClinicWorkingHours(DateTime appointmentTime)
    {
        var workingHours = new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0));
        if (workingHours.Contains(appointmentTime.ToTimeOnly())) return;
        throw new BusinessException(AppointmentExceptionCodes.MustBeWithinWorkingHourOfClinic, "");
    }

    private void GuardAgainstAppointmentDurationNotAppropriateToTheDoctorSpeciality(IDoctor doctor,
        TimeSpan duration)
    {
        if (doctor.AggregateState.DurationConstraint.Contains(duration)) return;
        throw new BusinessException(AppointmentExceptionCodes.MustBeAppropriateToTheDoctorSpeciality, "");
    }

    private void GuardAgainstAppointmentTimeDuringDoctorNotPresents(IDoctor doctor, DateTime appointmentTime)
    {
        WeeklySchedule weeklySchedule = doctor.AggregateState.WeeklySchedule;
        if (weeklySchedule.AcceptAppointmentTime(appointmentTime)) return;
        throw new BusinessException(AppointmentExceptionCodes.MustBeADuringTheDoctorsPresents, "");
    }

    private void GuardAgainstPatientMoreThanTwoAppointmentAtTheSameDate(
        ImmutableArray<AppointmentEntity> patientsAppointments)
    {
        if (patientsAppointments.Length < 2) return;
        throw new BusinessException(AppointmentExceptionCodes.PatientMustBeLessThanTwoAppointmentAtTheSameDay, "");
    }

    private void GuardAgainstAppointmentsOfPatientHasNotOverlap(ImmutableArray<AppointmentEntity> patientsAppointments,
        DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        if (patientsAppointments.HasNotOverlapWith(appointmentTime, appointmentDuration)) return;
        throw new BusinessException(AppointmentExceptionCodes.AppointmentsOfPatientShouldNotOverlap, "");
    }

    private void GuardAgainstInvalidOverlappingAppointment(IDoctor doctor, DateTime appointmentTime,
        TimeSpan appointmentDuration)
    {
        if (AggregateState.Appointments.HasNotOverlapWith(appointmentTime, appointmentDuration)) return;
        if (AggregateState.NumberOfOverlappingAppointment <
            doctor.AggregateState.NumberOfAllowedOverlappingAppointment) return;
        throw new BusinessException(
            AppointmentExceptionCodes
                .TheNumberOfDoctorsOverlappingAppointmentsShouldNotExceededTheAllowedNumberOfOverlappingAppointments,
            "");
    }

    #endregion
}

public static class AppointmentEnumerationExtensions
{
    public static bool HasNotOverlapWith(this IEnumerable<AppointmentEntity> source, DateTime appointmentTime,
        TimeSpan appointmentDuration)
    {
        var (start, end) =
            (appointmentTime.ToTimeOnly(), appointmentTime.ToTimeOnly().Add(appointmentDuration));
        return source.Select(a => new Range<TimeOnly>(a.AppointmentTime.ToTimeOnly(),
                a.AppointmentTime.ToTimeOnly().Add(a.AppointmentDuration)))
            .All(a => !a.Contains(start) && !a.Contains(end));
    }
}