using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Common.Logging;
using Foosball9000Api.RequestResponse;
using Logic;
using Models;
using MongoDBRepository;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MatchController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILeaderboardViewRepository _leaderboardViewRepository;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly ISeasonLogic _seasonLogic;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchupResultRepository _matchupResultRepository;

        public MatchController(IMatchRepository matchRepository,
            IMatchupResultRepository matchupResultRepository,
            ILeaderboardService leaderboardService,
            ILeaderboardViewRepository leaderboardViewRepository,
            ILogger logger,
            IUserRepository userRepository,
            ISeasonLogic seasonLogic)
        {
            _matchRepository = matchRepository;
            _matchupResultRepository = matchupResultRepository;
            _leaderboardService = leaderboardService;
            _leaderboardViewRepository = leaderboardViewRepository;
            _logger = logger;
            _userRepository = userRepository;
            _seasonLogic = seasonLogic;
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
        public IEnumerable<Match> LastGames([FromUri] int numberOfMatches)
        {
            var seasons = _seasonLogic.GetSeasons();

            //TODO remove!
            if (seasons.Count == 0)
            {
                _seasonLogic.CreateSeasons();
                _seasonLogic.ConvertOldMatches();
            }

            return _matchRepository.GetRecentMatches(numberOfMatches);
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(SaveMatchesRequest saveMatchesRequest)
        {
            if (saveMatchesRequest == null)
            {
                return BadRequest();
            }
            //var validated = _userRepository.Validate(saveMatchesRequest.User);
            //if (!validated)
            //{
            //    return Unauthorized();
            //}

            var seasons = _seasonLogic.GetSeasons();

            if (seasons.All(x => x.EndDate != null))
            {
                return BadRequest("No active seaons");
            }

            var currentSeason = seasons.Single(x => x.EndDate != null);

            var matches = saveMatchesRequest.Matches.OrderBy(x => x.TimeStampUtc).ToList();

            //Sat i AddMatch java
            foreach (var match in matches)
            {
                if (match.TimeStampUtc == DateTime.MinValue)
                {
                    match.TimeStampUtc = DateTime.UtcNow;
                }
                
                match.SeasonName = currentSeason.Name;

                LeaderboardView currentLeaderboard = _leaderboardService.GetLatestLeaderboardView();

                if (match.Id != null)
                {
                    _leaderboardService.AddMatchToLeaderboard(currentLeaderboard, match);
                    _leaderboardViewRepository.SaveLeaderboardView(currentLeaderboard);
                }
                
                _matchRepository.SaveMatch(match);

                _leaderboardService.RecalculateLeaderboard();

            }
            
            return Ok();
        }

        [HttpGet]
        public MatchupResult GetMatchupResult(List<string> userlist)
        {
            try
            {
                userlist = new List<string> { "jasper@sovs.net", "maso@seges.dk", "madsskipper@gmail.com", "anjaskott@gmail.com"};

                //Sort
                    var sortedUserlist = userlist.OrderBy(x => x).ToList();
                var addedList = string.Join("", sortedUserlist.ToArray());

                //RecalculateLeaderboard hashstring
                var hashcode = addedList.GetHashCode();

                //RecalculateLeaderboard the correct one
                var results = _matchupResultRepository.GetByHashResult(hashcode);

                //TODO dont seem optimal to create a list every time
                var team1list = new List<string> {userlist[0], userlist[1]};
                var team1Hashcode = team1list.OrderBy(x => x).GetHashCode();

                var team2list = new List<string> {userlist[3], userlist[4]};
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

        [HttpGet]
        public IEnumerable<Match> TodaysMatches()
        {
            return _matchRepository.GetMatchesByTimeStamp(DateTime.Today);
        }

        [HttpGet]
        public Match GetMatch(Guid guid)
        {
            if (guid == null) return null;
            return _matchRepository.GetMatchById(guid);
        }
    }
}