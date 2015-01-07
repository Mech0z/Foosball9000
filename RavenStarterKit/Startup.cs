using System.Configuration;
using d60.Cirqus;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Config;
using d60.Cirqus.MongoDb.Config;
using d60.Cirqus.MongoDb.Events;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.Serialization;
using d60.Cirqus.Views;
using Microsoft.Owin;
using MongoDB.Driver;
using MvcPWy;
using MvcPWy.Commands;
using MvcPWy.PViews;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace MvcPWy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var connStr = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongoClient = new MongoClient(connStr);
            var mongoServer = mongoClient.GetServer();
            var MongoDatabase = mongoServer.GetDatabase("futtrader2");


            var eventStore = new MongoDbEventStore(MongoDatabase, "Events");
            var repository = new DefaultAggregateRootRepository(eventStore, new JsonDomainEventSerializer(),
                new DefaultDomainTypeNameMapper());

            var viewManager = new MongoDbViewManager<LeaderboardView>(MongoDatabase, "LeaderboardView");
            var eventDispatcher = new ViewManagerEventDispatcher(repository, eventStore, new JsonDomainEventSerializer(),
                new DefaultDomainTypeNameMapper(), viewManager);

            var processor = CommandProcessor.With()
                .EventStore(e => e.UseMongoDb(connStr, "Events"))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManager))
                .Create();

            processor.ProcessCommand(new AddMatch())
        }
    }
}