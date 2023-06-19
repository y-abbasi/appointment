using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Tests.Appointments;

public class TheNumberOfDoctorsOverlappingAppointmentsShouldNotExceededTheAllowedNumberOfOverlappingAppointments
{
    private AppointmentManager _appointmentManager = new();

    private readonly WeeklySchedule _weeklySchedule = new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
        .Add(new DailySchedule(1,
            ImmutableArray<Range<TimeOnly>>.Empty
                .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
        )));

    void GivenAPatientHasBeenDefined()
    {
        _appointmentManager.ThereIsAPatient();
    }

    void GivenADoctorHasBeenDefined(DoctorSpeciality doctorSpeciality)
    {
        _appointmentManager = new();
        _appointmentManager.ThereIsADoctor(doctorSpeciality, _weeklySchedule);
    }

    async Task GivenPatientSetAppointmentBefore(int numberOfOverlappingAppointments)
    {
        var appointmentTime = DateTime.Parse("2023-12-18 10:00");
        for (var i = 0; i <= numberOfOverlappingAppointments; i++)
        {
            await _appointmentManager.SetAppointment(appointmentTime, TimeSpan.FromMinutes(15), PatientId.New());
            appointmentTime = appointmentTime.Add(TimeSpan.FromMinutes(10));
        }
    }

    void WhenISetAppointmentAtSpecificTime()
    {
        var appointmentTime = DateTime.Parse("2023-12-18 10:00");
        _appointmentManager.TryToSetAppointment(appointmentTime, TimeSpan.FromMinutes(10));
    }

    async Task ThenExceptionShouldBeThrown()
    {
        await _appointmentManager.ExceptionWasThrow<BusinessException>(AppointmentExceptionCodes
            .TheNumberOfDoctorsOverlappingAppointmentsShouldNotExceededTheAllowedNumberOfOverlappingAppointments);
    }
}