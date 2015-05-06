using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Logic;
using Models;

namespace FoosballOld.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LeaderboardController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<LeaderboardViewEntry> Index()
        {
            var leaderboard = _leaderboardService.GetLatestLeaderboardView();
            return leaderboard.Entries;
        }
    }
}
