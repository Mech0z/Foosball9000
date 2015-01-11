using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcPWy.AggRoots;
using MvcPWy.Cirqus;
using MvcPWy.Commands;
using MvcPWy.Models;

namespace MvcPWy.Controllers
{
    public class MatchController : Controller
    {
        private readonly ICommandProcessorThing _commandProcessorThing;

        public MatchController(ICommandProcessorThing commandProcessorThing)
        {
            _commandProcessorThing = commandProcessorThing;
        }

        public ActionResult Index()
        {
            var matchViewModel = new MatchViewModel
            {
                Usernames = CreatePlayerList(GetUsers())
            };

            return View(matchViewModel);
        }

        private List<ApplicationUser> GetUsers()
        {
            var session = RavenContext.CreateSession();
            return session.Query<ApplicationUser>().Take(1024).ToList();
        } 

        private IEnumerable<SelectListItem> CreatePlayerList(List<ApplicationUser> _playerList)
        {
            return _playerList.Select(p => new SelectListItem
            {
                Text = p.UserName,
                Value = p.UserName
            });
        }

        [HttpPost]
        public ActionResult Create(MatchViewModel vm)
        {
            var users = GetUsers();

            var guid = Guid.NewGuid();
            var match = new Match
            {
                MatchResults = new MatchResult { Team1Score = vm.MatchResults.Team1Score, Team2Score = vm.MatchResults.Team2Score},
                StaticFormation = vm.StaticFormation,
                Team1 = new List<ApplicationUser> { users.Single(x => x.UserName == vm.Player1), users.Single(x => x.UserName == vm.Player2) },
                Team2 = new List<ApplicationUser> { users.Single(x => x.UserName == vm.Player3), users.Single(x => x.UserName == vm.Player4) }
            };

            _commandProcessorThing.Processor.ProcessCommand(new AddMatch(guid.ToString(), match));

            return RedirectToAction("Index", "LeaderBoard");
        }
    }
}