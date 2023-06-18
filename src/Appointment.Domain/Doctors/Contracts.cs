using DevArt.Core.Domain;

namespace Appointment.Domain.Doctors;

public interface IDoctor : IAggregateRoot<IDoctorState, DoctorId>
{
}

public record DoctorDefinedEvent(DoctorId AggregateId, DoctorSpeciality DoctorSpeciality, WeeklySchedule WeeklySchedule,
    long Version) : DomainEvent<DoctorId>(AggregateId, "Doctor", Version);

public record DoctorId(string Value) : IIdentifier
{
    public string PersistenceId => $"Doctor-{Value}";

    public static DoctorId New() => new DoctorId(Guid.NewGuid().ToString());
}

public enum DoctorSpeciality
{
    GeneralPractitioner,
    Specialist
}

public interface IDoctorService
{
    Task<IDoctor> GetById(DoctorId doctorId);
}