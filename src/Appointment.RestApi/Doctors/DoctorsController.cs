using Appointment.Application.Appointments;
using Appointment.Application.Doctors;
using Appointment.Domain;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Application;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.RestApi.Doctors;

[ApiController]
[Route("[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly ICommandDispatcher _commandBus;

    public DoctorsController(ICommandDispatcher commandBus)
    {
        _commandBus = commandBus;
    }

    [HttpPost]
    public async Task<string> Create(DefineDoctor setAppointment)
    {
        var command = setAppointment.ToCommand();
        await _commandBus.Dispatch<DoctorAggregateActor, DoctorId>(command);
        return command.Id.Value;
    }
}

public record DefineDoctor(DoctorSpeciality DoctorSpeciality, WeeklySchedule WeeklySchedule)
{
    public DefineDoctorCommand ToCommand() => new(DoctorId.New(),
        DoctorSpeciality, WeeklySchedule, new UserId(""), new TenantId(""));
}