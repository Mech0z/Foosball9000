using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Models;
using MongoDBRepository;
using System;

namespace FoosballOld.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MatchController : ApiController
    {
        private readonly IMatchRepositoryV2 _matchRepository;
        private readonly IMatchupResultRepository _matchupResultRepository;

        public MatchController(IMatchRepositoryV2 matchRepository, IMatchupResultRepository matchupResultRepository)
        {
            _matchRepository = matchRepository;
            _matchupResultRepository = matchupResultRepository;
        }

        // GET: /api/Match/GetAll
        [HttpGet]
        public IEnumerable<MatchV2> GetAll()
        {
            return _matchRepository.GetMatches();
        }

        // GET: /api/Match/LastGames?numberOfMatches=10
        [HttpGet]
        public IEnumerable<MatchV2> LastGames([FromUri]int numberOfMatches)
        {
            return _matchRepository.GetRecentMatches(numberOfMatches);
        }

        [HttpPost]
        public IHttpActionResult SaveMatch(MatchV2 match)
        {
            match.TimeStampUtc = DateTime.UtcNow;

            _matchRepository.SaveMatch(match);

            //Run validation

            return Ok();
        }

        [HttpGet]
        public MatchupResult GetMatchupResult(List<string> userlist)
        {
            //Sort
            var sortedUserlist = userlist.OrderBy(x => x).ToList();
            var addedList = string.Join("", sortedUserlist.ToArray());

            //Get hashstring
            var hashcode = addedList.GetHashCode();

            //Get the correct one
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
    }
}