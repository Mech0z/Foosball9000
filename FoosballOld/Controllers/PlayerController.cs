using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PlayerController : ApiController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;

        public PlayerController(IMatchRepository matchRepository,
            IUserRepository userRepository)
        {
            _matchRepository = matchRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<Match> GetPlayerMatches(string email)
        {
            return _matchRepository.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
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
                return Conflict();
            }

            _userRepository.AddUser(user);
            return Ok();
        }
    }
}