using Appointment.Application.Appointments;
using Appointment.Domain;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Application;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.RestApi.Appointments;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ICommandDispatcher _commandBus;

    public AppointmentsController(ICommandDispatcher commandBus)
    {
        _commandBus = commandBus;
    }

    [HttpPost]
    public async Task Create(SetAppointment setAppointment)
    {
        var command = setAppointment.ToCommand();
        await _commandBus.Dispatch<AppointmentAggregateActor, AppointmentId>(command);
        //return command.
    }
}

public record SetAppointment(string DoctorId, string PatientId, DateTime AppointmentTime, int Duration)
{
    public SetAppointmentCommand ToCommand() => new SetAppointmentCommand(
        new AppointmentId(AppointmentTime.ToDateOnly(), new DoctorId(DoctorId)), new PatientId(PatientId),
        AppointmentTime, TimeSpan.FromMinutes(Duration), new UserId(""), new TenantId(""));
}