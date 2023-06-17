namespace Appointment.specs.Features.Appointments.Models;

public record AppointmentModel(string Patient, string Doctor, DateTime AppointmentTime, int AppointmentDuration);