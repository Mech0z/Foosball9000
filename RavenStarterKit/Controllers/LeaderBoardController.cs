using System.Configuration;
using System.Web.Mvc;
using d60.Cirqus.MongoDb.Views;
using MongoDB.Driver;
using MvcPWy.PViews;

namespace MvcPWy.Controllers
{
    public class LeaderBoardController : Controller
    {
        private MongoDatabase _mongoDatabase;

        public LeaderBoardController()
        {
            var connStr = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongoClient = new MongoClient(connStr);
            var mongoServer = mongoClient.GetServer();
            _mongoDatabase = mongoServer.GetDatabase("foosball9000");
        }

        // GET: LeaderBoard
        public ActionResult Index()
        {
            var viewManager = new MongoDbViewManager<LeaderboardView>(_mongoDatabase, "LeaderboardView");
            LeaderboardView test = viewManager.Load("__global__");
            return View(test);
        }
    }
}
