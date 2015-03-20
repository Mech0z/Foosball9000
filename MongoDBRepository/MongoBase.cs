using MongoDB.Driver;

namespace MongoDBRepository
{
    public class MongoBase<T> : IMongo
    {
        protected MongoDatabase MongoDatabase;
        protected MongoCollection<T> Collection;

        public MongoBase(string collectionName)
        {
            var mongoClient = new MongoClient(ConnectionString.Default.mongoconnection);
            var mongoServer = mongoClient.GetServer();
            MongoDatabase = mongoServer.GetDatabase(ConnectionString.Default.database);

            Collection = MongoDatabase.GetCollection<T>(collectionName);
        }
    }
}