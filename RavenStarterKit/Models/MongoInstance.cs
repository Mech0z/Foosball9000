using System.Configuration;
using MongoDB.Driver;

namespace MvcPWy.Models
{
    public class MongoInstance : IMongoInstance
    {
        private readonly MongoDatabase _mongoDatabase;
        private readonly string _connectionString;

        public MongoInstance()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongoClient = new MongoClient(_connectionString);
            var mongoServer = mongoClient.GetServer();
            _mongoDatabase = mongoServer.GetDatabase("foosball9000");
        }

        public MongoDatabase GetDatabase()
        {
            return _mongoDatabase;
        }

        public string GetConnectionstring()
        {
            return _connectionString;
        }
    }
}