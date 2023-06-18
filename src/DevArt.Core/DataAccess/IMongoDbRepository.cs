using MongoDB.Driver;

namespace DevArt.Core.DataAccess;

public interface IMongoDbRepository<TDocument> : IRepository<TDocument>
{
    IEnumerable<TDocument> FilterBy(
        FilterDefinition<TDocument> filter);

}