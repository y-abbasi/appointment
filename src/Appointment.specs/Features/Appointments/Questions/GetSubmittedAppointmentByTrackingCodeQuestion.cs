using Appointment.specs.Features.Appointments.Models;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Interactions;
using Suzianna.Rest.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments.Questions;

internal class GetSubmittedAppointmentByTrackingCodeQuestion: IQuestion<AppointmentResponse>
{
    private readonly DateOnly _appointmentDate;
    private readonly string _doctorId;
    private readonly string _trackingCode;

    public GetSubmittedAppointmentByTrackingCodeQuestion(DateOnly appointmentDate, string doctorId, string trackingCode)
    {
        _appointmentDate = appointmentDate;
        _doctorId = doctorId;
        _trackingCode = trackingCode;
    }

    public AppointmentResponse AnsweredBy(Actor actor)
    {
        Get.ResourceAt($"appointments/{_appointmentDate:yyyy-MM-dd}/{_doctorId}/{_trackingCode}")
            .PerformAs(actor);
        return LastResponse.Content<AppointmentResponse>().AnsweredBy(actor);

    }
}