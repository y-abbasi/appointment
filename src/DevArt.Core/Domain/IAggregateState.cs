using System.Collections.Immutable;
using DevArt.Core.Domain.Constraints;

namespace DevArt.Core.Domain;

public interface IAggregateState<TKey> 
    where TKey : IValueObject
{
    TKey Id { get; }
    long Version { get; init; }
    ImmutableList<IConstraint> GetConstraints();
}