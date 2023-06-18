namespace DevArt.Core.Domain;

public interface IAggregateDeletedEvent<out TKey> : IDomainEvent<TKey> where TKey : IValueObject
{
}