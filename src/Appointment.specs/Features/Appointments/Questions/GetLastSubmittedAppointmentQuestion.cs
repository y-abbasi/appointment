using Appointment.specs.Features.Appointments.Models;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments.Questions;

internal class GetLastSubmittedAppointmentQuestion : IQuestion<AppointmentResponse>
{
    public AppointmentResponse AnsweredBy(Actor actor)
    {
        var id = actor.AsksFor(LastResponse.Raw()).Content.ReadAsStringAsync().Result;
        return new GetSubmittedAppointmentByIdQuestion(id).AnsweredBy(actor);
    }
}