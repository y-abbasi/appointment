using DevArt.Core.Domain;

namespace Appointment.Domain.Doctors;

public class Doctor : AggregateRoot<IDoctorState, DoctorId>, IDoctor
{
    public Doctor(DoctorId id) : base(id, new DoctorNotInitializedState(id))
    {
    }

    public override IDomainEvent<DoctorId> Process(IDomainEvent<DoctorId> @event)
    {
        dynamic evt = @event;
        AggregateState = AggregateState.Apply(evt);
        return evt;
    }

    public override Task<IEnumerable<IDomainEvent<DoctorId>>> Do(object arg)
    {
        dynamic msg = arg;
        return Do(msg);
    }
}