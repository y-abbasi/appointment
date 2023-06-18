using Appointment.Domain.Doctors;
using DevArt.Core.Application;
using DevArt.Core.Queries;

namespace Appointment.Application.Doctors;

public class DoctorAggregateActor  : AggregateManager<Doctor, IDoctorState, DoctorId, IDoctorCommand>
{
    public DoctorAggregateActor(DoctorId id) : base(new Doctor(id))
    {
        Command<GetById<DoctorId>>(_ => Sender.Tell(Aggregate.AggregateState, Self));
    }

    protected override object? MapToArg(IDoctorCommand cmd)
    {
        return cmd switch
        {
            DefineDoctorCommand command => command.ToArg(),
            _ => null
        };
    }
}