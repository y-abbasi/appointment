using System.Collections.Immutable;
using DevArt.Core.Domain.Constraints;

namespace DevArt.Core.Domain;

public interface IAggregateRoot<TKey>
    where TKey : IIdentifier
{
    TKey Id { get; }
}

public interface IAggregateRoot<TState, TKey> : IDo<object, TKey>, IAggregateRoot<TKey>
    where TState : IAggregateState<TKey>
    where TKey : IIdentifier
{
    TState AggregateState { get; }
    IDomainEvent<TKey> Process(IDomainEvent<TKey> @event);
    ImmutableList<IConstraint> GetConstraints() => AggregateState.GetConstraints();
}