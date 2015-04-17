using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IMatchRepositoryV2 _matchRepositoryV2;

        public PlayerController(IMatchRepositoryV2 matchRepositoryV2)
        {
            _matchRepositoryV2 = matchRepositoryV2;
        }

        [HttpGet]
        public IEnumerable<MatchV2> GetPlayerMatches(string email)
        {
            return _matchRepositoryV2.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
        }
    }
}
