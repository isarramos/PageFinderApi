using Domain;
using Domain.Repository;
using Microsoft.Extensions.Options;
using Mongo.Settings;
using MongoDB.Driver;

namespace Mongo
{
    public class BookRepository : IRepository<BookReview>
    {
        private readonly DatabaseSettings _settings;
        private readonly IMongoCollection<BookReview> _collection;

        public BookRepository(IMongoService service, IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<BookReview>(_settings.BookReviewCollectionName);
        }

        public async Task InsertAsync(BookReview entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(BookReview entity, CancellationToken cancellationToken)
        {
            var filter = Builders<BookReview>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(BookReview entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<BookReview> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

    }
}