using Microsoft.AspNetCore.Mvc;

namespace Appointment.RestApi.Appointments;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    public Task<Guid> Create(SetAppointment setAppointment)
    {
        return Task.FromResult(Guid.Empty);
    }
}

public record SetAppointment(Guid DoctorId, Guid PatientId, DateTime AppointmentTime, TimeSpan Duration)
{
    
}