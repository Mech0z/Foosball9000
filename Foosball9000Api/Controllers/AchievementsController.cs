using System.Web.Http;
using Logic;
using Models;

namespace Foosball9000Api.Controllers
{
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
