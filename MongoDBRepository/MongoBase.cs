using MongoDB.Driver;

namespace MongoDBRepository
{
    public class MongoBase<T> : IMongo
    {
        protected MongoDatabase MongoDatabase;
        protected MongoCollection<T> Collection;

        public MongoBase(string collectionName)
        {

            var mongoClient = new MongoClient(mongoConnection);
            var mongoServer = mongoClient.GetServer();
            MongoDatabase = mongoServer.GetDatabase(database);

            Collection = MongoDatabase.GetCollection<T>(collectionName);
        }
    }
}