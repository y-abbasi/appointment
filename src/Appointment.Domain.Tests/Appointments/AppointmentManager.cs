using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Domain;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Appointment.Domain.Tests.Appointments;
using Appointment = Domain.Appointments.Appointment;
internal class AppointmentManager
{
    private DoctorId? _doctorId;
    private PatientId? _patientId;
    private Func<Task> _lastAction;
    private IEnumerable<IDomainEvent<AppointmentId>> _raisedEvent;

    public void ThereIsAPatient()
    {
        _patientId = PatientId.New();
    }

    public void ThereIsADoctor(DoctorSpeciality doctorSpeciality, WeeklySchedule weeklySchedule)
    {
        _doctorId = DoctorId.New();
    }

    public void TryToSetAppointment(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        _lastAction = async () =>
        {
            var rule = new Appointment(new AppointmentId(appointmentTime.ToDateOnly(), _doctorId!));
            _raisedEvent = await rule.Do(new SetAppointmentArg(_patientId!, appointmentTime, appointmentDuration));
        };
    }

    public AppointmentManager AfterThat()
    {
        _lastAction().GetAwaiter().GetResult();
        return this;
    }

    public void AppointmentSetsProperly(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        var expected = new AppointmentSetsEvent(new AppointmentId(appointmentTime.ToDateOnly(), _doctorId), _patientId,
            appointmentTime, appointmentDuration, 1);
        _raisedEvent.First().Should()
            .BeEquivalentTo(expected, ExcludeEventMetadata);
    }

    EquivalencyAssertionOptions<T> ExcludeEventMetadata<T>(EquivalencyAssertionOptions<T> options) where T : IDomainEvent
    {
        return options.Excluding(e => e.Version)
            .Excluding(e => e.Publisher)
            .Excluding(e => e.PublishedAt)
            .Excluding(e => e.TenantId);
    }
}