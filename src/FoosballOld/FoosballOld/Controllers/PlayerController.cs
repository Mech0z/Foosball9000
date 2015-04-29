using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Logic;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayerController : ApiController
    {
        private readonly ICreateLeaderboardViewV2 _leaderboardViewV2;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchRepositoryV2 _matchRepositoryV2;
        private readonly IUserRepository _userRepository;

        public PlayerController(IMatchRepository matchRepository, ICreateLeaderboardViewV2 leaderboardViewV2,
            IMatchRepositoryV2 matchRepositoryV2, IUserRepository userRepository)
        {
            _matchRepository = matchRepository;
            _leaderboardViewV2 = leaderboardViewV2;
            _matchRepositoryV2 = matchRepositoryV2;
            _userRepository = userRepository;
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

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            var users = _userRepository.GetUsers();

            if (users.Any(x => x.Email == user.Email))
            {
                return BadRequest();
            }

            _userRepository.AddUser(user);
            return Ok();
        }
    }
}