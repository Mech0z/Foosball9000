using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Logic;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MatchController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILeaderboardViewRepository _leaderboardViewRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchupResultRepository _matchupResultRepository;

        public MatchController(IMatchRepository matchRepository,
            IMatchupResultRepository matchupResultRepository,
            ILeaderboardService leaderboardService,
            ILeaderboardViewRepository leaderboardViewRepository)
        {
            _matchRepository = matchRepository;
            _matchupResultRepository = matchupResultRepository;
            _leaderboardService = leaderboardService;
            _leaderboardViewRepository = leaderboardViewRepository;
        }

        // GET: /api/Match/GetAll
        [HttpGet]
        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetMatches();
        }

        // GET: /api/Match/LastGames?numberOfMatches=10
        [HttpGet]
        public IEnumerable<Match> LastGames([FromUri] int numberOfMatches)
        {
            return _matchRepository.GetRecentMatches(numberOfMatches);
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(Match match)
        {
            match.TimeStampUtc = DateTime.UtcNow;

            //TODO Run validation

            _matchRepository.SaveMatch(match);

            var currentLeaderboard = _leaderboardService.GetLatestLeaderboardView();

            _leaderboardService.AddMatchToLeaderboard(currentLeaderboard, match);

            _leaderboardViewRepository.SaveLeaderboardView(currentLeaderboard);

            return Ok();
        }

        [HttpGet]
        public MatchupResult GetMatchupResult(List<string> userlist)
        {
            //Sort
            var sortedUserlist = userlist.OrderBy(x => x).ToList();
            var addedList = string.Join("", sortedUserlist.ToArray());

            //RecalculateLeaderboard hashstring
            var hashcode = addedList.GetHashCode();

            //RecalculateLeaderboard the correct one
            var results = _matchupResultRepository.GetByHashResult(hashcode);

            // TODO dont seem optimal to create a list every time
            var team1List = new List<string> {userlist[0], userlist[1]};
            var team1Hashcode = team1List.OrderBy(x => x).GetHashCode();

            var team2List = new List<string> {userlist[3], userlist[4]};
            var team2Hashcode = team2List.OrderBy(x => x).GetHashCode();

            var result =
                results.Single(x =>
                    (x.Team1HashCode == team1Hashcode || x.Team1HashCode == team2Hashcode) &&
                    (x.Team2HashCode == team1Hashcode || x.Team2HashCode == team2Hashcode));

            return result;
        }
    }
}