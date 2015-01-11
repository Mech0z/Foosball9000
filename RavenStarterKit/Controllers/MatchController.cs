using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcPWy.Models;

namespace MvcPWy.Controllers
{
    public class MatchController : Controller
    {
        // GET: Match
        public ActionResult Index()
        {
            var matchViewModel = new MatchViewModel
            {
                Usernames = CreatePlayerList()
            };

            return View(matchViewModel);
        }

        private IEnumerable<SelectListItem> CreatePlayerList()
        {
            var session = RavenContext.CreateSession();
            var playerList = session.Query<ApplicationUser>().Take(1024).ToList();

            return playerList.Select(p => new SelectListItem
            {
                Text = p.UserName,
                Value = p.UserName
            });
        }

        [HttpPost]
        public ActionResult Create(MatchViewModel vm)
        {
            throw new System.NotImplementedException();
        }
    }
}