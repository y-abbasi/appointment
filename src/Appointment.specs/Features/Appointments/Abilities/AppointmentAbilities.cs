using Appointment.specs.Features.Appointments.Models;
using Appointment.specs.Features.Appointments.Questions;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;

namespace Appointment.specs.Features.Appointments.Abilities;

public class AppointmentAbilities : IAppointmentAbilities
{
    public IPerformable SetAppointment(SetAppointmentCommand command)
    {
        return Post.DataAsJson(command).To($"appointments");
    }

    public IQuestion<AppointmentResponse> GetLastCreatedAppointment()
    {
        return new GetLastSubmittedAppointmentQuestion();
    }
}