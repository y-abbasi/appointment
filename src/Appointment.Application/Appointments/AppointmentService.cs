using System.Collections.Immutable;
using Akka.Actor;
using Akka.Persistence;
using Appointment.Domain.Appointments;
using Appointment.Domain.Patiens;

namespace Appointment.Application.Appointments;

public class AppointmentService : IAppointmentService
{
    private readonly ActorSystem _actorSystem;

    public AppointmentService(ActorSystem actorSystem)
    {
        _actorSystem = actorSystem;
    }

    public Task<ImmutableArray<AppointmentEntity>> GetPatientAppointmentsInDay(PatientId patientId,
        DateOnly appointmentDate)
    {
        var actorRef =
            _actorSystem.ActorOf(Props.Create(() => new PatientAppointmentActor(appointmentDate, patientId)));
        return actorRef.Ask<ImmutableArray<AppointmentEntity>>(PatientAppointmentActor.GetAppointments.Instance);
    }
}

public class PatientAppointmentActor : ReceivePersistentActor
{
    public record GetAppointments
    {
        private GetAppointments()
        {
        }

        public static GetAppointments Instance { get; } = new();
    }

    private ImmutableArray<AppointmentEntity> _appointments = ImmutableArray<AppointmentEntity>.Empty;

    public PatientAppointmentActor(DateOnly appointmentDate, PatientId patientId)
    {
        PersistenceId = $"PatientAppointment-{appointmentDate}-{patientId.Value}";
        Recover<AppointmentSetsEvent>(@event =>
            _appointments = _appointments.Add(new AppointmentEntity(@event.AggregateId.DoctorId, @event.PatientId,
                @event.AppointmentTime, @event.AppointmentDuration)));
        Command<GetAppointments>(_ => Sender.Tell(_appointments));
    }

    public override string PersistenceId { get; }
}