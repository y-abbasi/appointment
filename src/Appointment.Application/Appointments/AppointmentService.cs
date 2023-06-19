using System.Collections.Immutable;
using Akka.Actor;
using Akka.Persistence;
using Akka.Persistence.EventStore;
using Akka.Serialization;
using Appointment.Domain.Appointments;
using Appointment.Domain.Patiens;
using EventStore.ClientAPI;

namespace Appointment.Application.Appointments;

public class AppointmentService : IAppointmentService
{
    private readonly ActorSystem _actorSystem;
    private readonly IEventStoreConnection _eventStoreConnection;
    private readonly Serialization _serialization;
    private readonly DefaultEventAdapter _eventAdapter;

    public AppointmentService(ActorSystem actorSystem, IEventStoreConnection eventStoreConnection)
    {
        _actorSystem = actorSystem;
        _eventStoreConnection = eventStoreConnection;
        _serialization = actorSystem.Serialization;
        _eventAdapter = new DefaultEventAdapter(_serialization);
    }

    public async Task<ImmutableArray<AppointmentEntity>> GetPatientAppointmentsInDay(PatientId patientId,
        DateOnly appointmentDate)
    {
        var stream = $"PatientAppointment-{appointmentDate:yyyyMMdd}-{patientId.Value}";
        var events = await _eventStoreConnection.ReadStreamEventsForwardAsync(stream, 0, 1024, true);
        var appointments = ImmutableArray<AppointmentEntity>.Empty;
        foreach (var resolvedEvent in events.Events)
        {
            if (_eventAdapter.Adapt(resolvedEvent).Payload is AppointmentSetsEvent evt)
                appointments = appointments.Add(new AppointmentEntity(@evt.TrackingCode, @evt.AggregateId.DoctorId, @evt.PatientId,
                    @evt.AppointmentTime, @evt.AppointmentDuration));
        }
        return appointments;
    }
}