using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Models;
using MongoDBRepository;

namespace Foosball.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PlayerController : Controller
    {
        private readonly IMatchRepositoryV2 _matchRepositoryV2;

        public PlayerController(IMatchRepositoryV2 matchRepositoryV2)
        {
            _matchRepositoryV2 = matchRepositoryV2;
        }
        /// <summary>
        /// http://localhost:30197/api/player/GetPlayerMatches?email=some@email.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MatchV2> GetPlayerMatches(string email)
        {
            return _matchRepositoryV2.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
        }
    }
}