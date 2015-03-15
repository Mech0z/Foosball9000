using Foosball.Models;
using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using Foosball.Models;

namespace Foosball.Repository
{
    public class MongoBase<T> : IMongo
    {
        protected MongoDatabase MongoDatabase;
        protected MongoCollection<T> Collection;

        public MongoBase(string collectionName, IOptions<Settings> settings)
        {
            var mongoClient = new MongoClient(settings.Options.MongoConnection);
            var mongoServer = mongoClient.GetServer();
            MongoDatabase = mongoServer.GetDatabase(settings.Options.Database);

            Collection = MongoDatabase.GetCollection<T>(collectionName);
        }
    }
}