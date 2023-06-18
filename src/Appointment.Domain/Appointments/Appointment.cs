using DevArt.Core.Domain;

namespace Appointment.Domain.Appointments;

public class Appointment:AggregateRoot<IAppointmentState, AppointmentId>, IAppointment
{
    public Appointment(AppointmentId id) : base(id, new AppointmentInitializedState(id, 0))
    {
    }

    public override IDomainEvent<AppointmentId> Process(IDomainEvent<AppointmentId> @event)
    {
        dynamic evt = @event;
        AggregateState = AggregateState.Apply(evt);
        return evt;

    }

    public override Task<IEnumerable<IDomainEvent<AppointmentId>>> Do(object arg)
    {
        dynamic msg = arg;
        return Do(msg);
    }

    public async Task<IEnumerable<IDomainEvent<AppointmentId>>> Do(SetAppointmentArg arg)
    {
        return new List<IDomainEvent<AppointmentId>>()
        {
            new AppointmentSetsEvent(Id, arg.PatientId, arg.AppointmentTime, arg.AppointmentDuration, AggregateState.Version + 1)
        };
    }
}