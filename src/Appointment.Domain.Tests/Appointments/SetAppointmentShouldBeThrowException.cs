using Appointment.Domain.Doctors;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Tests.Appointments;

public class SetAppointmentShouldBeThrowException
{
    private AppointmentManager _appointmentManager = new();

    void GivenAPatientHasBeenDefined()
    {
        _appointmentManager.ThereIsAPatient();
    }

    void GivenADoctorHasBeenDefined(DoctorSpeciality doctorSpeciality, WeeklySchedule weeklySchedule)
    {
        _appointmentManager = new();
        _appointmentManager.ThereIsADoctor(doctorSpeciality, weeklySchedule);
    }

    void WhenISetAppointmentAtSpecificTime(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        _appointmentManager.TryToSetAppointment(appointmentTime, appointmentDuration);
    }

    async Task ThenExceptionShouldBeThrown(string exceptionCode)
    {
        await _appointmentManager.ExceptionWasThrow<BusinessException>(exceptionCode);
    }
}