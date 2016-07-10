using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Foosball9000Api.RequestResponse;
using Logic;
using Models;
using MongoDBRepository;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PlayerController : ApiController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchupHistoryCreator _matchupHistoryCreator;
        private readonly ISeasonLogic _seasonLogic;

        public PlayerController(IMatchRepository matchRepository,
            IUserRepository userRepository, 
            IMatchupHistoryCreator matchupHistoryCreator, 
            ISeasonLogic seasonLogic)
        {
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _matchupHistoryCreator = matchupHistoryCreator;
            _seasonLogic = seasonLogic;
        }

        [HttpGet]
        public IEnumerable<Match> GetPlayerMatches(string email)
        {
            return _matchRepository.GetPlayerMatches(email).OrderByDescending(x => x.TimeStampUtc);
        }

        [HttpGet]
        public List<PartnerPercentResult> GetPlayerPartnerResults(string email)
        {
            var activeSeason = _seasonLogic.GetActiveSeason();

            return _matchupHistoryCreator.GetPartnerWinPercent(email, activeSeason.Name);
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

        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            var hash = _userRepository.Login(user);
            if (hash == string.Empty)
            {
                return Unauthorized();
            }
            
            return Ok(hash);
        }

        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePasswordRequest request)
        {
            if (request.NewPassword.Length < 6)
            {
                return BadRequest("Password too short");
            }

            var hash = _userRepository.ChangePassword(request.Email, request.OldPassword, request.NewPassword);
            
            if (hash == string.Empty)
            {
                return Unauthorized();
            }
            
            return Ok(hash);
        }
    }
}