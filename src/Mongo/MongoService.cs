using Microsoft.Extensions.Options;
using Mongo.Settings;
using MongoDB.Driver;

namespace Mongo
{
    public class MongoService : IMongoService
    {
        public DatabaseSettings Settings { get; set; }
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoService(IOptions<DatabaseSettings> settings)
        {
            Settings = settings.Value;
            Client = new MongoClient(Settings.ConnectionString);
            Database = Client.GetDatabase(Settings.DatabaseName);
        }
    }
}
