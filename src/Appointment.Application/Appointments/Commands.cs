using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using DevArt.Core.Application;
using DevArt.Core.IdentityAccess;

namespace Appointment.Application.Appointments;

public record SetAppointmentCommand(AppointmentId Id, PatientId PatientId, DateTime AppointmentTime,
    TimeSpan AppointmentDuration, UserId UserId, TenantId TenantId) : IAppointmentCommand
{
    public SetAppointmentArg ToArg(IAppointmentService appointmentService, IDoctorService doctorService) =>
        new SetAppointmentArg(PatientId, AppointmentTime, AppointmentDuration,
            appointmentService, doctorService);
}

public interface IAppointmentCommand : ICommand<AppointmentId>
{
}