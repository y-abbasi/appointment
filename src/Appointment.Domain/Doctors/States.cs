using System.Collections.Immutable;
using DevArt.Core.Domain;
using DevArt.Core.ErrorHandling;

namespace Appointment.Domain.Doctors;

public interface IDoctorState : IAggregateState<DoctorId>, IApplyEvent<DoctorDefinedEvent, IDoctorState>
{
    Range<TimeSpan> DurationConstraint { get; }
    WeeklySchedule WeeklySchedule { get; }
    int NumberOfAllowedOverlappingAppointment { get;  }
}

public record DoctorNotInitializedState(DoctorId Id) : AggregateState<DoctorId>(Id), IDoctorState
{
    Range<TimeSpan> IDoctorState.DurationConstraint => throw new BusinessException("BR-1001", "Entity not found.");

    WeeklySchedule IDoctorState.WeeklySchedule => throw new BusinessException("BR-1001", "Entity not found.");
    int IDoctorState.NumberOfAllowedOverlappingAppointment => throw new BusinessException("BR-1001", "Entity not found.");

    public IDoctorState Apply(DoctorDefinedEvent @event)
    {
        return @event.DoctorSpeciality switch
        {
            DoctorSpeciality.GeneralPractitioner => new DoctorGeneralPractitionerState(Id, @event.WeeklySchedule),
            DoctorSpeciality.Specialist => new DoctorSpecialistState(Id, @event.WeeklySchedule),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public abstract record DoctorInitializedState
    (DoctorId Id, WeeklySchedule WeeklySchedule) : AggregateState<DoctorId>(Id), IDoctorState
{
    public abstract Range<TimeSpan> DurationConstraint { get; }
    public abstract int NumberOfAllowedOverlappingAppointment { get; }

    public IDoctorState Apply(DoctorDefinedEvent @event)
    {
        throw new BusinessException("BR-1000", "Entity already exists.");
    }
}

public record DoctorGeneralPractitionerState(DoctorId Id, WeeklySchedule WeeklySchedule) : DoctorInitializedState(Id,
    WeeklySchedule)
{
    public override Range<TimeSpan> DurationConstraint =>
        new Range<TimeSpan>(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15));

    public override int NumberOfAllowedOverlappingAppointment => 2;
}

public record DoctorSpecialistState(DoctorId Id, WeeklySchedule WeeklySchedule) : DoctorInitializedState(Id,
    WeeklySchedule)
{
    public override Range<TimeSpan> DurationConstraint =>
        new Range<TimeSpan>(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(30));

    public override int NumberOfAllowedOverlappingAppointment => 3;
}