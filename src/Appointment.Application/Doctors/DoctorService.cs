using Appointment.Domain.Doctors;
using DevArt.Core.Akka;
using DevArt.Core.Queries;

namespace Appointment.Application.Doctors;

public class DoctorService : IDoctorService
{
    private readonly ActorRefProvider<DoctorAggregateActor> _doctorAggregateActor;

    public DoctorService(ActorRefProvider<DoctorAggregateActor> doctorAggregateActor)
    {
        _doctorAggregateActor = doctorAggregateActor;
    }
    public Task<IDoctorState> GetById(DoctorId doctorId)
    {
        return _doctorAggregateActor.Ask<IDoctorState>(new GetById<DoctorId>(doctorId));
    }
}