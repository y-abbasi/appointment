using System.Linq.Expressions;
using DevArt.Core.Domain;
using EventStore.ClientAPI;

namespace DevArt.Core.DataAccess;

public interface IEsRepository
{
    Task<IEnumerable<IDomainEvent>> GetEventsByStreamName(string streamName);
    Task WriteToStream(string streamName, IEnumerable<IDomainEvent> @events);
}
public interface IEsRepository<TAgg, in TKey> where TKey : IIdentifier
{
    Task<TAgg> GetById(TKey id);
    Task Append(IDomainEvent<TKey> events);
}
