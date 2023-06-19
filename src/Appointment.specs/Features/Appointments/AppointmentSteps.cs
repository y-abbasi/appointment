using Appointment.specs.Features.Appointments.Abilities;
using Appointment.specs.Features.Appointments.Models;
using FluentAssertions;
using Suzianna.Core.Screenplay;
using Suzianna.Core.Screenplay.Actors;
using Suzianna.Rest.Screenplay.Questions;

namespace Appointment.specs.Features.Appointments;

[Binding]
public class AppointmentSteps
{
    private readonly ScenarioContext _context;
    private readonly IAppointmentAbilities _appointmentAbilities;
    private readonly Actor _actor;

    public AppointmentSteps(Stage stage, ScenarioContext context, IAppointmentAbilities appointmentAbilities)
    {
        _context = context;
        _appointmentAbilities = appointmentAbilities;
        _actor = stage.ActorInTheSpotlight;
    }

    [Given(@"'(.*)' overlapping appointments with the following properties has already been registered")]
    public void GivenOverlappingAppointmentsWithTheFollowingPropertiesHasAlreadyBeenRegistered(
        int numberOfRegisteredAppointment, List<SetAppointmentCommand> appointments)
    {
        var registered = 0;
        foreach (var appointment in appointments)
        {
            if (registered > numberOfRegisteredAppointment) return;
            registered++;
            _actor.AttemptsTo(_appointmentAbilities.SetAppointment(appointment));
        }
    }

    [Given(@"An appointment with the following properties has already been registered")]
    [When(@"I set appointment with the following properties")]
    public void WhenISetAppointmentWithTheFollowingProperties(SetAppointmentCommand command)
    {
        _context.Set(command);
        _actor.AttemptsTo(_appointmentAbilities.SetAppointment(command));
    }


    [Then(@"I can find an appointment with above info")]
    public void ThenICanFindAnAppointmentWithAboveInfo()
    {
        var expected = _context.Get<SetAppointmentCommand>();
        var actual =
            _actor.AsksFor(_appointmentAbilities.GetLastCreatedAppointment(expected.AppointmentTime.ToDateOnly(),
                expected.DoctorId));
        actual.Should().BeEquivalentTo(new
        {
            expected.DoctorId, expected.PatientId, expected.AppointmentTime,
            Duration = TimeSpan.FromMinutes(expected.Duration)
        });
    }

    [Then(@"Exception with the code '(.*)' should be thrown")]
    public void ThenExceptionWithTheCodeShouldBeThrown(string expectedErrorCode)
    {
        var error = _actor.AsksFor(LastResponse.Content<BusinessError>());
        error.ErrorCode.Should().Be(expectedErrorCode);
    }
}
public record BusinessError(string ErrorCode, string ErrorMessage);