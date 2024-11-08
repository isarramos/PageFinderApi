using Domain;
using Domain.Repository;
using Microsoft.Extensions.Options;
using Mongo.Settings;
using MongoDB.Driver;

namespace Mongo
{
    public class UserRepository : IRepository<User>
    {
        private readonly DatabaseSettings _settings;
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoService service, IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<User>(_settings.UserCollectionName);
        }

        public async Task InsertAsync(User entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(User entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

    }
}