using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Logic;
using Models;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LeaderboardController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<LeaderboardView> Index()
        {
                return _leaderboardService.GetLatestLeaderboardViews();
        }
    }
}