using System.Collections.Immutable;
using DevArt.Core.Domain;

namespace Appointment.Domain.Appointments;

public interface IAppointmentState : IAggregateState<AppointmentId>,
    IApplyEvent<AppointmentSetsEvent, IAppointmentState>
{
    ImmutableArray<AppointmentEntity> Appointments { get; }
    int NumberOfOverlappingAppointment { get; }
}

public record AppointmentInitializedState(AppointmentId Id, long Version) : IAppointmentState
{
    public IAppointmentState Apply(AppointmentSetsEvent @event)
    {
        return this with
        {
            Appointments = Appointments.Add(new AppointmentEntity(@event.TrackingCode, Id.DoctorId, @event.PatientId,
                @event.AppointmentTime, @event.AppointmentDuration)),
            NumberOfOverlappingAppointment = NumberOfOverlappingAppointment +
                                             (Appointments.HasNotOverlapWith(@event.AppointmentTime,
                                                 @event.AppointmentDuration)
                                                 ? 0
                                                 : 1)
        };
    }

    public ImmutableArray<AppointmentEntity> Appointments { get; private init; } =
        ImmutableArray<AppointmentEntity>.Empty;

    public int NumberOfOverlappingAppointment { get; private init; }
}