using System.Collections.Immutable;
using Appointment.specs.Features.Doctors.Models;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Features.Doctors;

[Binding]
public class Transformers
{
    private readonly Actor _actor;

    public Transformers(Stage stage)
    {
        _actor = stage.ActorInTheSpotlight;
    }
    
    [StepArgumentTransformation]
    public DoctorModel DoctorModel(Table table)
    {
        return table.CreateInstance<DoctorModel>();
    }
    
    [StepArgumentTransformation]
    public WeeklySchedule WeeklySchedule(Table table)
    {
        return new WeeklySchedule(table.CreateSet<DailySchedule>().ToImmutableArray());
    }
    
}