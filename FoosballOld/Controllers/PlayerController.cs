using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Common.Logging;
using Logic;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PlayerController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILogger _logger;
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;

        public PlayerController(ILeaderboardService leaderboardService,
            IMatchRepository matchRepository,
            IUserRepository userRepository,
            ILogger logger)
        {
            _leaderboardService = leaderboardService;
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Match> GetPlayerMatches(string email)
        {
            try
            {
                return _matchRepository.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occured");
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occured");
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            try
            {
                var users = _userRepository.GetUsers();

                if (users.Any(x => x.Email == user.Email))
                {
                    return Conflict();
                }

                _userRepository.AddUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occured");
                throw;
            }
        }
    }
}