namespace Appointment.Domain;

public record Range<T>(T Start, T End) where T : IComparable<T>
{
    public bool Contains(T value)
    {
        return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
    }
}