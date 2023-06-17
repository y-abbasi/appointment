using Appointment.specs.Features.Doctors.Abilities;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;

namespace Appointment.specs.Features.Doctors;

using Models;

[Binding]
public class Steps
{
    private readonly Stage _stage;
    private readonly IDoctorAbilities _doctorAbilities;
    private readonly Actor _actor;

    public Steps(Stage stage, IDoctorAbilities doctorAbilities)
    {
        _stage = stage;
        _doctorAbilities = doctorAbilities;
        _actor = _stage.ActorInTheSpotlight;
    }

    [Given(@"A Doctor has been defined with the following properties")]
    public void GivenADoctorHasBeenDefinedWithTheFollowingProperties(DoctorModel doctor)
    {
        var builder = new DoctorBuilder(doctor.Name, doctor.DoctorSpeciality);
        _actor.Remember("doctor-builder", builder);
    }

    [Given(@"With the following weekly schedule")]
    public void GivenWithTheFollowingWeeklySchedule(WeeklySchedule weeklySchedule)
    {
        var builder = _actor.Recall<DoctorBuilder>("doctor-builder") with
        {
            WeeklySchedule = weeklySchedule
        };
        _actor.Remember("doctor-builder", builder);
    }

    [Given(@"I have registered the doctor '(.*)'")]
    public void GivenIHaveRegisteredTheDoctor(string smith)
    {
        var builder = _actor.Recall<DoctorBuilder>("doctor-builder");
        _actor.AttemptsTo(_doctorAbilities.DefineDoctor(builder.Build()));
    }
}