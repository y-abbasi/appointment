using DevArt.Core.Domain;

namespace DevArt.Core.Queries;

public interface IReadModel<TKey>
    where TKey : IIdentifier
{
    TKey Id { get; init; }
    long Version { get; }
    bool Apply(EventMessage msg);
}