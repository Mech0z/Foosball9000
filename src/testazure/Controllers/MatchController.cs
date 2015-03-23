using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Models;
using MongoDBRepository;

namespace Foosball.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MatchController : Controller
    {
        private readonly IMatchRepository _matchRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetMatches();
        }

        [HttpGet]
        public IEnumerable<Match> LastGames([FromHeader]int numberOfMatches)
        {
            var matches = _matchRepository.GetRecentMatches(numberOfMatches);

            return matches;
        }

        [HttpPost]
        public void SaveDraft([FromBody]Match match)
        {
            var existingMatch = _matchRepository.GetByTimeStamp(match.TimeStampUtc);

            if (existingMatch == null)
            {
                return;
            }

            _matchRepository.SaveMatch(match);
        }   
    }
}