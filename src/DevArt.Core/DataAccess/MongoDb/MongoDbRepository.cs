using System.Linq.Expressions;
using MongoDB.Driver;

namespace DevArt.Core.DataAccess.MongoDb;

public class MongoDbRepository<TDocument> : IMongoDbRepository<TDocument>
{
    private readonly IMongoCollection<TDocument> _collection;

    public MongoDbRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filter)
    {
        return _collection.Find(filter).ToEnumerable();
    }

    public async Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        var result = await _collection.FindAsync(filterExpression);
        return result.ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TDocument? FindOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).FirstOrDefaultAsync();
    }

    public virtual void InsertOne(TDocument document)
    {
        _collection.InsertOne(document);
    }

    public virtual Task InsertOneAsync(TDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    public void InsertMany(ICollection<TDocument> documents)
    {
        _collection.InsertMany(documents);
    }

    public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public void ReplaceOne(Expression<Func<TDocument, bool>> filterExpression, TDocument document)
    {
        _collection.FindOneAndReplace(filterExpression, document);
    }

    public virtual async Task ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression, TDocument document)
    {
        await _collection.FindOneAndReplaceAsync(filterExpression, document);
    }

    public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        _collection.FindOneAndDelete(filterExpression);
    }

    public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    {
        _collection.DeleteMany(filterExpression);
    }

    public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }

    private protected string? GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute?)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName ?? documentType.Name.Replace("ReadModel", "");
    }
}