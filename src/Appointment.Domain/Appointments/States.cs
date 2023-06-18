using System.Collections.Immutable;
using DevArt.Core.Domain;
using DevArt.Core.Domain.Constraints;

namespace Appointment.Domain.Appointments;

public interface IAppointmentState : IAggregateState<AppointmentId>,
    IApplyEvent<AppointmentSetsEvent, IAppointmentState>
{
    
}

public record AppointmentInitializedState(AppointmentId Id, long Version) : IAppointmentState
{
    public ImmutableList<IConstraint> GetConstraints() => ImmutableList<IConstraint>.Empty;
    public IAppointmentState Apply(AppointmentSetsEvent @event)
    {
        throw new NotImplementedException();
    }
}