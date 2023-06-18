using System.Collections.Immutable;

namespace DevArt.Core.Domain;

public interface IAggregateState<TKey> 
    where TKey : IValueObject
{
    TKey Id { get; }
    long Version { get; init; }
}