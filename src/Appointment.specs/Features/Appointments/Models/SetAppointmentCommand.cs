namespace Appointment.specs.Features.Appointments.Models;

public record SetAppointmentCommand(Guid DoctorId, Guid PatientId, DateTime AppointmentTime, TimeSpan Duration);