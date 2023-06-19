using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Tests.Appointments;

public class AppointmentsOfPatientShouldNotOverlap
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

    async Task GivenPatientSetAppointmentBefore(DateTime firstAppointmentTime, TimeSpan firstAppointmentDuration)
    {
        await _appointmentManager.SetAppointment(firstAppointmentTime, firstAppointmentDuration);
    }

    void WhenISetAppointmentAtSpecificTime(DateTime secondAppointmentTime, TimeSpan secondAppointmentDuration)
    {
        _appointmentManager.TryToSetAppointment(secondAppointmentTime, secondAppointmentDuration);
    }

    async Task ThenExceptionShouldBeThrown()
    {
        await _appointmentManager.ExceptionWasThrow<BusinessException>(AppointmentExceptionCodes
            .AppointmentsOfPatientShouldNotOverlap);
    }
}