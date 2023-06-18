using Appointment.specs.Features.Appointments.Abilities;
using Appointment.specs.Features.Doctors.Abilities;
using Appointment.specs.Supports.Retrievers;
using BoDi;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Screenplay.Abilities;
using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Supports;

[Binding]
public class TestDependencies
{
    private readonly IObjectContainer _container;
    private readonly ScenarioContext _scenarioContext;

    public TestDependencies(IObjectContainer container, ScenarioContext scenarioContext)
    {
        _container = container;
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun(Order = 0)]
    public static void Configure()
    {
        Service.Instance.ValueRetrievers.Register(new TimeRangesRetriever());
    }

    [BeforeScenario(Order = 0)]
    public void SetupStage()
    {
        var cast = Cast.WhereEveryoneCan(new List<IAbility>
        {
            CallAnApi.At("http://localhost:5180")
        });
        var stage = new Stage(cast);
        stage.ShineSpotlightOn("Martin");
        _container.RegisterInstanceAs(cast);
        _container.RegisterInstanceAs(stage);
        _container.RegisterTypeAs<AppointmentAbilities, IAppointmentAbilities>();
        _container.RegisterTypeAs<DoctorAbilities, IDoctorAbilities>();
    }

    [BeforeStep]
    public void WaitBeforeSteps()
    {
        Task.Delay(TimeSpan.FromMilliseconds(150))
            .GetAwaiter()
            .GetResult();
    }
}