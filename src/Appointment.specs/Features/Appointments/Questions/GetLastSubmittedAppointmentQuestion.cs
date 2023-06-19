using Appointment.specs.Features.Appointments.Models;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Core.Screenplay.Questions;
using Suzianna.Rest.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments.Questions;

internal class GetLastSubmittedAppointmentQuestion : IQuestion<AppointmentResponse>
{
    private readonly DateOnly _appointmentDate;
    private readonly string _doctorId;

    public GetLastSubmittedAppointmentQuestion(DateOnly appointmentDate, string doctorId)
    {
        _appointmentDate = appointmentDate;
        _doctorId = doctorId;
    }
    public AppointmentResponse AnsweredBy(Actor actor)
    {
        var trackingCode = actor.AsksFor(LastResponse.Raw()).Content.ReadAsStringAsync().Result;
        return new GetSubmittedAppointmentByTrackingCodeQuestion(_appointmentDate, _doctorId, trackingCode).AnsweredBy(actor);
    }
}