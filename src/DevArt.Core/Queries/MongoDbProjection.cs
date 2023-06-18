using Akka.Actor;
using DevArt.Core.DataAccess;
using DevArt.Core.Domain;

namespace DevArt.Core.Queries;

public class MongoDbProjection<TKey, TModel> : ReceiveActor
    where TModel : IReadModel<TKey>, new() where TKey : IIdentifier
{
    protected readonly IMongoDbRepository<TModel> Repository;
    private readonly Dictionary<string, long> _aggregateVersions = new();

    public MongoDbProjection(IMongoDbRepository<TModel> repository)
    {
        Repository = repository;
        ReceiveAsync<EventMessage>(async message =>
        {
            try
            {
                var evt = (IDomainEvent<TKey>)message.Event;
                if (_aggregateVersions.ContainsKey(evt.AggregateId.Value) &&
                    message.SequenceNr <= _aggregateVersions[evt.AggregateId.Value])
                    return;
                if (evt is IAggregateDeletedEvent<TKey>)
                {
                    await Repository.DeleteOneAsync(m => m.Id.Value == evt.AggregateId.Value);
                    return;
                }

                var model = await Repository
                                .FindOneAsync(m => m.Id.Value == evt.AggregateId.Value) ??
                            new TModel { Id = evt.AggregateId };
                if (!model.Apply(message)) return;
                if (model.Version == 1)
                    await Repository.InsertOneAsync(model);
                else
                    await Repository.ReplaceOneAsync(m => m.Id.Value == evt.AggregateId.Value, model);
                _aggregateVersions[evt.AggregateId.Value] = message.SequenceNr;
            }
            finally
            {
                Sender.Tell(true);
            }
        });
    }
}