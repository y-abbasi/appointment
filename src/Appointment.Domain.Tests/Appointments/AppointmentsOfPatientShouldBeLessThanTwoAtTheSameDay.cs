using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Tests.Appointments;

public class AppointmentsOfPatientShouldBeLessThanTwoAtTheSameDay
{
    private AppointmentManager _appointmentManager = new();
    private readonly DoctorSpeciality _doctorSpeciality = DoctorSpeciality.Specialist;

    private readonly WeeklySchedule _weeklySchedule = new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
        .Add(new DailySchedule(1,
            ImmutableArray<Range<TimeOnly>>.Empty
                .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
        )));

    void GivenAPatientHasBeenDefined()
    {
        _appointmentManager.ThereIsAPatient();
    }

    void GivenADoctorHasBeenDefined()
    {
        _appointmentManager = new();
        _appointmentManager.ThereIsADoctor(_doctorSpeciality, _weeklySchedule);
    }

    async Task GivenPatientSetTwoAppointmentAtOneDay()
    {
        await _appointmentManager.SetAppointment(DateTime.Parse("2023-12-18 9:00"), TimeSpan.FromMinutes(10));
        await _appointmentManager.SetAppointment(DateTime.Parse("2023-12-18 10:00"), TimeSpan.FromMinutes(10));
    }

    void WhenISetAppointmentAtSpecificTime(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        _appointmentManager.TryToSetAppointment(appointmentTime, appointmentDuration);
    }

    async Task ThenExceptionShouldBeThrown()
    {
        await _appointmentManager.ExceptionWasThrow<BusinessException>(AppointmentExceptionCodes
            .PatientMustBeLessThanTwoAppointmentAtTheSameDay);
    }
}