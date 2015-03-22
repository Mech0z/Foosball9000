using Foosball.Logic;
using Microsoft.AspNet.Mvc;
using Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Foosball.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LeaderboardController : Controller
    {
        private readonly ICreateLeaderboardView _createLeaderboardView;

        public LeaderboardController(ICreateLeaderboardView createLeaderboardView)
        {
            _createLeaderboardView = createLeaderboardView;
        }

        // GET: /<controller>/
        [HttpGet]
        public LeaderboardView Index()
        {
            var leaderboard = _createLeaderboardView.Get(true);
            return leaderboard;
        }


    }
}
