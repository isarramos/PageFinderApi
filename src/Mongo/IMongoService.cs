using Mongo.Settings;
using MongoDB.Driver;

namespace Mongo
{
    public interface IMongoService
    {
        DatabaseSettings Settings { get; set; }
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}
