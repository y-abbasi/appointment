namespace Appointment.specs.Features.Appointments.Models;

public record AppointmentResponse(Guid DoctorId, Guid PatientId, DateTime AppointmentTime, TimeSpan Duration);