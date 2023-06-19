namespace Appointment.specs.Features.Appointments.Models;

public record SetAppointmentCommand(string DoctorId, string PatientId, DateTime AppointmentTime, int Duration);