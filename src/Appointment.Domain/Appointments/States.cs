using System.Collections.Immutable;
using DevArt.Core.Domain;

namespace Appointment.Domain.Appointments;

public interface IAppointmentState : IAggregateState<AppointmentId>,
    IApplyEvent<AppointmentSetsEvent, IAppointmentState>
{
    
}

public record AppointmentInitializedState(AppointmentId Id, long Version) : IAppointmentState
{
    public IAppointmentState Apply(AppointmentSetsEvent @event)
    {
        throw new NotImplementedException();
    }
}