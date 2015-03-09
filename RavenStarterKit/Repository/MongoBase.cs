using MongoDB.Driver;

namespace MvcPWy.Repository
{
    public class MongoBase<T> : IMongo
    {
        private readonly string _connStr;
        private readonly MongoClient _mongoClient;
        private readonly MongoServer _mongoServer;
        protected MongoDatabase MongoDatabase;
        protected MongoCollection<T> Collection;

        public MongoBase(string collectionName)
        {
            _connStr = "mongodb://foosball101:foosball101@ds031711.mongolab.com:31711/foosball9000";
            _mongoClient = new MongoClient(_connStr);
            _mongoServer = _mongoClient.GetServer();
            MongoDatabase = _mongoServer.GetDatabase("foosball9000");

            Collection = MongoDatabase.GetCollection<T>(collectionName);
        }
    }
}