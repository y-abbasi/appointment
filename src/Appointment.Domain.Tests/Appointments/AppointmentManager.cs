using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;
using DevArt.Core.ErrorHandling;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NSubstitute;

namespace Appointment.Domain.Tests.Appointments;

using Appointment = Domain.Appointments.Appointment;

internal class AppointmentManager
{
    private IDoctor _doctor = null!;
    private PatientId? _patientId;
    private Func<Task> _lastAction;
    private IEnumerable<IDomainEvent<AppointmentId>> _raisedEvent;
    private readonly IDoctorService _doctorService = Substitute.For<IDoctorService>();
    private readonly IAppointmentService _appointmentService = Substitute.For<IAppointmentService>();
    private Dictionary<AppointmentId, Appointment> _appointments = new();

    public AppointmentManager()
    {
        _appointmentService.GetPatientAppointmentsInDay(Arg.Any<PatientId>(), Arg.Any<DateOnly>())
            .Returns(Task.FromResult(ImmutableArray<AppointmentEntity>.Empty));
    }

    public void ThereIsAPatient()
    {
        _patientId = PatientId.New();
    }

    public void ThereIsADoctor(DoctorSpeciality doctorSpeciality, WeeklySchedule weeklySchedule)
    {
        _doctor = new Doctor(DoctorId.New());
        _doctor.Process(new DoctorDefinedEvent(_doctor.Id, doctorSpeciality, weeklySchedule, 1));
        _doctorService.GetById(_doctor.Id).Returns(Task.FromResult(_doctor.AggregateState));
    }


    public void TryToSetAppointment(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        _lastAction = async () =>
        {
            var id = new AppointmentId(appointmentTime.ToDateOnly(), _doctor.Id);
            var appointment = GetAppointment(id);
            _raisedEvent =
                await appointment.Do(new SetAppointmentArg("",_patientId!, appointmentTime, appointmentDuration,
                    _appointmentService, _doctorService));
        };
    }

    private Appointment GetAppointment(AppointmentId id)
    {
        return _appointments.ContainsKey(id) ? _appointments[id] : (_appointments[id] = new Appointment(id));
    }

    public AppointmentManager AfterThat()
    {
        _lastAction().GetAwaiter().GetResult();
        return this;
    }

    public async Task ExceptionWasThrow<T>(string code) where T : BusinessException
    {
        await _lastAction.Should().ThrowAsync<T>().Where(ex => ex.Code == code);
    }

    public void AppointmentSetsProperly(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        var expected = new AppointmentSetsEvent(new AppointmentId(appointmentTime.ToDateOnly(), _doctor.Id),
            "",
            _patientId!,
            appointmentTime, appointmentDuration, 1);
        _raisedEvent.First().Should()
            .BeEquivalentTo(expected, ExcludeEventMetadata);
    }

    EquivalencyAssertionOptions<T> ExcludeEventMetadata<T>(EquivalencyAssertionOptions<T> options)
        where T : IDomainEvent
    {
        return options.Excluding(e => e.Version)
            .Excluding(e => e.Publisher)
            .Excluding(e => e.PublishedAt)
            .Excluding(e => e.TenantId);
    }

    public async Task SetAppointment(DateTime appointmentTime, TimeSpan appointmentDuration,
        PatientId? patientId = null)
    {
        patientId ??= _patientId;
        var id = new AppointmentId(appointmentTime.ToDateOnly(), _doctor.Id);
        var appointment = GetAppointment(id);
        appointment.Process(new AppointmentSetsEvent(new AppointmentId(appointmentTime.ToDateOnly(), _doctor.Id),
            "",
            patientId!,
            appointmentTime, appointmentDuration, 1));
        var appointments =
            await _appointmentService.GetPatientAppointmentsInDay(patientId!, appointmentTime.ToDateOnly());
        appointments = appointments.Add(new("A-100", _doctor.Id, patientId!, appointmentTime, appointmentDuration));
        _appointmentService.GetPatientAppointmentsInDay(patientId!, appointmentTime.ToDateOnly())
            .Returns(Task.FromResult(appointments));
    }
}