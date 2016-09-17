using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Foosball9000Api.RequestResponse;
using Logic;
using Models;
using MongoDBRepository;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SeasonsAdministrationController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ISeasonLogic _seasonLogic;

        public SeasonsAdministrationController(IUserRepository userRepository, ISeasonLogic seasonLogic)
        {
            _userRepository = userRepository;
            _seasonLogic = seasonLogic;
        }

        [HttpPost]
        public IHttpActionResult StartNewSeason(VoidRequest request)
        {
            var validated = _userRepository.ValidateAndHasRole(new User
            {
                Email = request.Email,
                Password = request.Password
            }, "Admin");

            if (!validated)
            {
                return Unauthorized();
            }
            
            var seasonName = _seasonLogic.StartNewSeason();

            return Ok(seasonName);
        }

        [HttpPost]
        public IHttpActionResult GetSeasons(VoidRequest request)
        {
            var validated = _userRepository.ValidateAndHasRole(new User
            {
                Email = request.Email,
                Password = request.Password
            }, "Admin");

            if (!validated)
            {
                return Unauthorized();
            }

            List<Season> seasons = _seasonLogic.GetSeasons();
            return Ok(seasons);
        }
    }
}