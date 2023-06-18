namespace DevArt.Core.Domain;

public interface IDo<in TArg, TKey> where TKey : IValueObject
{
    Task<IEnumerable<IDomainEvent<TKey>>> Do(TArg arg);
}