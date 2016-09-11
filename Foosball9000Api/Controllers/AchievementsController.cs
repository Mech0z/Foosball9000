using System.Web.Http;
using Logic;
using Models;
using System.Web.Http.Cors;

namespace Foosball9000Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AchievementsController : ApiController
    {
        private readonly IAchievementsService _achievementsService;
        private readonly ISeasonLogic _seasonLogic;

        public AchievementsController(IAchievementsService achievementsService, ISeasonLogic seasonLogic)
        {
            _achievementsService = achievementsService;
            _seasonLogic = seasonLogic;
        }

        [HttpGet]
        public AchievementsView Index()
        {
            var activeSeason = _seasonLogic.GetActiveSeason();

            var ach = _achievementsService.GetAchievementsView(activeSeason.Name);

            return ach;
        }
    }
}
