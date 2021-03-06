﻿using System.Collections.Generic;
using Logic;
using Microsoft.AspNet.Mvc;
using Models;

namespace Foosball.Controllers
{
    [Route("api/[controller]")]
    public class LeaderboardController : Controller
    {
        private readonly ICreateLeaderboardViewV2 _createLeaderboardView;

        public LeaderboardController(ICreateLeaderboardViewV2 createLeaderboardView)
        {
            _createLeaderboardView = createLeaderboardView;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<LeaderboardViewEntry> Index()
        {
            var leaderboard = _createLeaderboardView.Get(true);
            return leaderboard.Entries;
        }
    }
}