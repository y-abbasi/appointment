using System.Collections.Immutable;
using DevArt.Core.Domain.Constraints;

namespace DevArt.Core.Domain;

public abstract record AggregateState<TKey>(TKey Id) : IAggregateState<TKey> where TKey : IValueObject
{
    public long Version { get; init; }
    public abstract ImmutableList<IConstraint> GetConstraints();
}