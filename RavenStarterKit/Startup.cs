using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
using MvcPWy.AggRoots;
using MvcPWy.Commands;
using MvcPWy.Controllers;
using MvcPWy.Models;
using MvcPWy.PViews;
using Owin;
using Raven.Client;

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
            var MongoDatabase = mongoServer.GetDatabase("foosball9000");




            var eventStore = new MongoDbEventStore(MongoDatabase, "Events");
            var repository = new DefaultAggregateRootRepository(eventStore, new JsonDomainEventSerializer(),
                new DefaultDomainTypeNameMapper());

            var viewManager = new MongoDbViewManager<LeaderboardView>(MongoDatabase, "LeaderboardView");
            //var eventDispatcher = new ViewManagerEventDispatcher(repository, eventStore, new JsonDomainEventSerializer(),
            //    new DefaultDomainTypeNameMapper(), viewManager);

            //var processor = CommandProcessor.With()
            //    .EventStore(e => e.UseMongoDb(connStr, "Events"))
            //    .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManager))
            //    .Create();

            

            //var session = RavenContext.CreateSession();
            //var player1 = session.Load<ApplicationUser>("ApplicationUsers/1");
            //var player2 = session.Load<ApplicationUser>("ApplicationUsers/33");
            //var player3 = session.Load<ApplicationUser>("ApplicationUsers/65");
            //var player4 = session.Load<ApplicationUser>("ApplicationUsers/66");

            //var guid = Guid.NewGuid();
            //var match = new Match
            //{
            //    MatchResults = new MatchResult() {Team1Score = 3, Team2Score = 10},
            //    StaticFormation = false,
            //    Team1 = new List<ApplicationUser>() {player1, player2},
            //    Team2 = new List<ApplicationUser>() {player3, player4}
            //};

            //processor.ProcessCommand(new AddMatch(guid.ToString(), match));

            //processor.Dispose();
        }
    }
}