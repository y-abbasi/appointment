using Suzianna.Core.Screenplay;

namespace Appointment.specs.Features.Patients;

[Binding]
public class Steps
{
    private readonly Stage _stage;

    public Steps(Stage stage)
    {
        _stage = stage;
    }

    [Given(@"There is a registered patient with the following properties")]
    public void GivenThereIsARegisteredPatientWithTheFollowingProperties(PatientModel patient)
    {
        var actor = _stage.ActorInTheSpotlight; 
        actor.Remember(patient.Name, patient);
    }
}