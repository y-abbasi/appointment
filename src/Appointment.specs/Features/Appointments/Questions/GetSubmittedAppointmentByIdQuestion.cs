using Appointment.specs.Features.Appointments.Models;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments.Questions;

internal class GetSubmittedAppointmentByIdQuestion: IQuestion<AppointmentResponse>
{
    private readonly Guid _id;

    public GetSubmittedAppointmentByIdQuestion(Guid id)
    {
        _id = id;
    }

    public AppointmentResponse AnsweredBy(Actor actor)
    {
        Get.ResourceAt($"api/appointment/{_id}")
            .PerformAs(actor);
        return LastResponse.Content<AppointmentResponse>().AnsweredBy(actor);

    }
}