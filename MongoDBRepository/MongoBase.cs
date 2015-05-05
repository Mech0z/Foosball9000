using MongoDB.Driver;
using System.Configuration;

namespace MongoDBRepository
{
    public class MongoBase<T> : IMongo
    {
        protected MongoDatabase MongoDatabase;
        protected MongoCollection<T> Collection;

        public MongoBase(string collectionName)
        {
            var mongoClient = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDb"].ToString());
            var mongoServer = mongoClient.GetServer();
            MongoDatabase = mongoServer.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]);

            Collection = MongoDatabase.GetCollection<T>(collectionName);
        }
    }
}