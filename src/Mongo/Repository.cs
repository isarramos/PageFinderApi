using Domain;
using Domain.Repository;
using Microsoft.Extensions.Options;
using Mongo.Settings;
using MongoDB.Driver;

namespace Mongo
{
    public class Repository : IRepository<Review>
    {
        private readonly DatabaseSettings _settings;
        private readonly IMongoCollection<Review> _collection;

        public Repository(IMongoService service, IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<Review>(_settings.CollectionName);
        }

        public async Task InsertAsync(Review entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Review entity, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Review entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<Review> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

    }
}