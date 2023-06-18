namespace DevArt.Core.Domain;

public abstract class AggregateRoot<TState, TKey> : IAggregateRoot<TState, TKey>
    where TState : IAggregateState<TKey>
    where TKey : IIdentifier
{
    public TKey Id { get; }

    protected AggregateRoot(TKey id, TState aggregateState)
    {
        Id = id;
        AggregateState = aggregateState;
    }

    public TState AggregateState { get; protected set; }

    public abstract IDomainEvent<TKey> Process(IDomainEvent<TKey> @event);
    
    public abstract Task<IEnumerable<IDomainEvent<TKey>>> Do(object arg);
}