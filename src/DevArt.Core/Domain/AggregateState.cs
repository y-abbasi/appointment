using System.Collections.Immutable;

namespace DevArt.Core.Domain;

public abstract record AggregateState<TKey>(TKey Id) : IAggregateState<TKey> where TKey : IValueObject
{
    public long Version { get; init; }
}