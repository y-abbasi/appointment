namespace Appointment.specs.Features.Appointments.Models;

public record SetAppointmentCommand(string DoctorId, Guid PatientId, DateTime AppointmentTime, int Duration);