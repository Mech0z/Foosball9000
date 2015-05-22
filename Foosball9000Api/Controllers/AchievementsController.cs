using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logic;
using Models;

namespace FoosballOld.Controllers
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
