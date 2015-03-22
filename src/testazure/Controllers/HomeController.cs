using System.Collections.Generic;
using Foosball.Logic;
using Microsoft.AspNet.Mvc;
using Models;

namespace Foosball.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreateLeaderboardView _createLeaderboardView;

        public HomeController(ICreateLeaderboardView createLeaderboardView)
        {
            _createLeaderboardView = createLeaderboardView;

            
        }

        public IEnumerable<LeaderboardViewEntry> Index()
        {
            var test = _createLeaderboardView.Get(true);
            
            return test.Entries;
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}