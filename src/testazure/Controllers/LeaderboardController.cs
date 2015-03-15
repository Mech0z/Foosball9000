using Microsoft.AspNet.Mvc;
using Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Foosball.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LeaderboardController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<LeaderboardView> Index()
        {
            return new List<LeaderboardView>();
        }
    }
}
