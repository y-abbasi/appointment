using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Tests.Doctors;

namespace Appointment.Domain.Tests.Appointments;

public class ShouldBeAbleToSetAppointmentProperly
{
    private AppointmentManager _appointmentManager;

    public ShouldBeAbleToSetAppointmentProperly()
    {
        _appointmentManager = new();
    }

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

    void ThenRuleCreatedProperly(DateTime appointmentTime, TimeSpan appointmentDuration)
    {
        _appointmentManager.AfterThat().AppointmentSetsProperly( appointmentTime, appointmentDuration);
    }
}