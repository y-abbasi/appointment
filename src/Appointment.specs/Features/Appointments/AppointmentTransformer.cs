using Appointment.specs.Features.Appointments.Models;
using Appointment.specs.Features.Doctors.Models;
using Appointment.specs.Features.Patients;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Features.Appointments;

[Binding]
public class AppointmentTransformer
{
    private readonly Actor _actor;

    public AppointmentTransformer(Stage stage)
    {
        _actor = stage.ActorInTheSpotlight;
    }
    [StepArgumentTransformation]
    public SetAppointmentCommand SetAppointmentCommand(Table table)
    {
        var model = table.CreateInstance<AppointmentModel>();
        return new(
            _actor.Recall<string>(model.Doctor),
            _actor.Recall<PatientModel>(model.Patient).Id,
            model.AppointmentTime,
            model.AppointmentDuration);
    }
    [StepArgumentTransformation]
    public List<SetAppointmentCommand> SetAppointmentCommands(Table table)
    {
        var models = table.CreateSet<AppointmentModel>();
        return models.Select(model => new SetAppointmentCommand(
            _actor.Recall<string>(model.Doctor),
            _actor.Recall<PatientModel>(model.Patient).Id,
            model.AppointmentTime,
            model.AppointmentDuration))
            .ToList();
    }
}