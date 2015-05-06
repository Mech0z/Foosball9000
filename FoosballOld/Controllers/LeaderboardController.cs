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
        private readonly ICreateLeaderboardViewV2 _createLeaderboardView;

        public LeaderboardController(ICreateLeaderboardViewV2 createLeaderboardView)
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
