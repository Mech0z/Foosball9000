using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Common.Logging;
using Logic;
using Models;
using MongoDBRepository;

namespace Foosball9000Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MatchController : ApiController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchupResultRepository _matchupResultRepository;
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILeaderboardViewRepository _leaderboardViewRepository;
        private readonly ILogger _logger;

        public MatchController(IMatchRepository matchRepository, 
            IMatchupResultRepository matchupResultRepository, 
            ILeaderboardService leaderboardService,
            ILeaderboardViewRepository leaderboardViewRepository,
            ILogger logger)
        {
            _matchRepository = matchRepository;
            _matchupResultRepository = matchupResultRepository;
            _leaderboardService = leaderboardService;
            _leaderboardViewRepository = leaderboardViewRepository;
            _logger = logger;
        }

        // GET: /api/Match/GetAll
        [HttpGet]
        public IEnumerable<Match> GetAll()
        {
            try
            {
                return _matchRepository.GetMatches();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{ExceptionSource} had an error", ex.Source);
                throw;
            }
        }

        // GET: /api/Match/LastGames?numberOfMatches=10
        [HttpGet]
        public IEnumerable<Match> LastGames([FromUri]int numberOfMatches)
        {
            try
            {
                return _matchRepository.GetRecentMatches(numberOfMatches);
            }
            catch (Exception ex)
            {
                _logger.Error(ex,"{ExceptionSource} had an error", ex.Source);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(Match match)
        {
            //Sat i AddMatch java

            if (match.TimeStampUtc == DateTime.MinValue)
            {
                match.TimeStampUtc = DateTime.UtcNow;
            }

            //TODO Run validation

            try
            {
                var currentLeaderboard = _leaderboardService.GetLatestLeaderboardView();
                
                _leaderboardService.AddMatchToLeaderboard(currentLeaderboard, match);
                
                _matchRepository.SaveMatch(match);

                _leaderboardViewRepository.SaveLeaderboardView(currentLeaderboard);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{ExceptionSource} had an error", ex.Source);
                throw;
            }
        }

        [HttpGet]
        public MatchupResult GetMatchupResult(List<string> userlist)
        {
            try
            {
                //Sort
                var sortedUserlist = userlist.OrderBy(x => x).ToList();
                var addedList = string.Join("", sortedUserlist.ToArray());

                //RecalculateLeaderboard hashstring
                var hashcode = addedList.GetHashCode();

                //RecalculateLeaderboard the correct one
                var results = _matchupResultRepository.GetByHashResult(hashcode);

                //TODO dont seem optimal to create a list every time
                var team1list = new List<string> { userlist[0], userlist[1] };
                var team1Hashcode = team1list.OrderBy(x => x).GetHashCode();

                var team2list = new List<string> { userlist[3], userlist[4] };
                var team2Hashcode = team2list.OrderBy(x => x).GetHashCode();

                var result =
                    results.Single(x =>
                    (x.Team1HashCode == team1Hashcode || x.Team1HashCode == team2Hashcode) &&
                    (x.Team2HashCode == team1Hashcode || x.Team2HashCode == team2Hashcode));

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{ExceptionSource} had an error", ex.Source);
                throw;
            }
        }
    }
}