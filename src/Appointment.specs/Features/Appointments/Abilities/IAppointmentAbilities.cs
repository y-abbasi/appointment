using Appointment.specs.Features.Appointments.Models;
using Appointment.specs.Features.Appointments.Questions;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments.Abilities;

public interface IAppointmentAbilities
{
    IPerformable SetAppointment(SetAppointmentCommand command);
    IQuestion<AppointmentResponse> GetLastCreatedAppointment();
}