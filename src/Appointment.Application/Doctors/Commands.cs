using Appointment.Domain;
using Appointment.Domain.Doctors;
using DevArt.Core.Application;
using DevArt.Core.IdentityAccess;

namespace Appointment.Application.Doctors;

public interface IDoctorCommand : ICommand<DoctorId>
{
    
}

public record DefineDoctorCommand(DoctorId Id, DoctorSpeciality DoctorSpeciality, WeeklySchedule WeeklySchedule, UserId UserId, TenantId TenantId) : IDoctorCommand
{
    public DefineDoctorArg ToArg() => new DefineDoctorArg(DoctorSpeciality, WeeklySchedule);
}