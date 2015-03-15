using System.Collections.Generic;
using Foosball.Models;
using Foosball.Repository;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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