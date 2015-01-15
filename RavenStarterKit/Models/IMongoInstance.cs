using MongoDB.Driver;

namespace MvcPWy.Models
{
    public interface IMongoInstance
    {
        MongoDatabase GetDatabase();
        string GetConnectionstring();
    }
}