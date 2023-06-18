using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using DevArt.Core.Application;
using DevArt.Core.Queries;

namespace Appointment.Application.Appointments;
using Appointment = Domain.Appointments.Appointment;

[Aggregate("Appointment")]
public class AppointmentAggregateActor : AggregateManager<Appointment, IAppointmentState, AppointmentId, IAppointmentCommand>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IDoctorService _doctorService;


    public AppointmentAggregateActor(AppointmentId id, IAppointmentService appointmentService, IDoctorService doctorService)
        : base(new Appointment(id))
    {
        _appointmentService = appointmentService;
        _doctorService = doctorService;
        Command<GetById<AppointmentId>>(_ => Sender.Tell(Aggregate.AggregateState, Self));
    }
    
    
    
    protected override object? MapToArg(IAppointmentCommand cmd)
    {
        return cmd switch
        {
            SetAppointmentCommand command => command.ToArg(_appointmentService, _doctorService),
            _ => null
        };
    }
}