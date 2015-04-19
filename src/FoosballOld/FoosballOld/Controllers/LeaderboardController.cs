using System.Collections.Generic;
using System.Web.Http;
using Logic;
using Models;

namespace FoosballOld.Controllers
{
    public class LeaderboardController : ApiController
    {
        private readonly ICreateLeaderboardView _createLeaderboardView;

        public LeaderboardController(ICreateLeaderboardView createLeaderboardView)
        {
            _createLeaderboardView = createLeaderboardView;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<LeaderboardViewEntry> Index()
        {
            var leaderboard = _createLeaderboardView.Get(true);
            return leaderboard.Entries;
        }
    }
}
