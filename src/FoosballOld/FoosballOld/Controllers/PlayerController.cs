using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Logic;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ICreateLeaderboardViewV2 _leaderboardViewV2;
        private readonly IMatchRepositoryV2 _matchRepositoryV2;

        public PlayerController(IMatchRepository matchRepository, ICreateLeaderboardViewV2 leaderboardViewV2, IMatchRepositoryV2 matchRepositoryV2)
        {
            _matchRepository = matchRepository;
            _leaderboardViewV2 = leaderboardViewV2;
            _matchRepositoryV2 = matchRepositoryV2;
        }

        [HttpGet]
        public IEnumerable<MatchV2> GetPlayerMatches(string email)
        {
            //var originalMatches = _matchRepository.GetMatches();

            //foreach (var match in originalMatches)
            //{
            //    match.Team1UserNames.AddRange(match.Team2UserNames);
            //    var matchv2 = new MatchV2
            //    {
            //        Id = match.Id,
            //        MatchResult = match.MatchResults,
            //        PlayerList = match.Team1UserNames,
            //        StaticFormationTeam1 = !match.StaticFormationTeam1,
            //        StaticFormationTeam2 = !match.StaticFormationTeam2,
            //        TimeStampUtc = match.TimeStampUtc
            //    };

            //    _matchRepositoryV2.SaveMatch(matchv2);
            //}

            return _matchRepositoryV2.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
        }

    }
}
