namespace Appointment.specs.Features.Appointments.Models;

public record AppointmentResponse(string DoctorId, string PatientId, DateTime AppointmentTime, TimeSpan Duration);