using System.Web.Http;
using Logic;
using Models;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Common.Logging;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AchievementsController : ApiController
    {
        private readonly IAchievementsService _achievementsService;

        public AchievementsController(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        [HttpGet]
        public AchievementsView Index()
        {
            var ach = _achievementsService.GetAchievementsView();

            return ach;
        }
    }
}
