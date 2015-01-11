using System.Configuration;
using d60.Cirqus;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Config;
using d60.Cirqus.MongoDb.Config;
using d60.Cirqus.MongoDb.Events;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.Serialization;
using d60.Cirqus.Views;
using MongoDB.Driver;
using MvcPWy.PViews;

namespace MvcPWy.Cirqus
{
    public class CommandProcessorThing : ICommandProcessorThing
    {
        public ICommandProcessor Processor { get; set; }

        public CommandProcessorThing()
        {
            var connStr = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongoClient = new MongoClient(connStr);
            var mongoServer = mongoClient.GetServer();
            var MongoDatabase = mongoServer.GetDatabase("foosball9000");

            var eventStore = new MongoDbEventStore(MongoDatabase, "Events");
            var repository = new DefaultAggregateRootRepository(eventStore, new JsonDomainEventSerializer(),
                new DefaultDomainTypeNameMapper());

            var viewManager = new MongoDbViewManager<LeaderboardView>(MongoDatabase, "LeaderboardView");
            var eventDispatcher = new ViewManagerEventDispatcher(repository, eventStore, new JsonDomainEventSerializer(),
                new DefaultDomainTypeNameMapper(), viewManager);

            Processor = CommandProcessor.With()
                .EventStore(e => e.UseMongoDb(connStr, "Events"))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManager))
                .Create();
        }
    }
}