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

        private IEnumerable<SelectListItem> CreatePlayerList(List<ApplicationUser> playerList)
        {
            return playerList.Select(p => new SelectListItem
            {
                Text = p.UserName,
                Value = p.UserName
            });
        }

        [HttpPost]
        public ActionResult Create(MatchViewModel vm)
        {
            var guid = Guid.NewGuid();
            var match = new Match
            {
                MatchResults = new MatchResult { Team1Score = vm.MatchResults.Team1Score, Team2Score = vm.MatchResults.Team2Score},
                StaticFormationTeam1 = vm.StaticFormationTeam1,
                StaticFormationTeam2 = vm.StaticFormationTeam2,
                Team1UserNames = new List<string> { vm.Player1, vm.Player2},
                Team2UserNames = new List<string> { vm.Player3, vm.Player4},
            };

            var commandResult = _commandProcessorThing.Processor.ProcessCommand(new AddMatch(guid.ToString(), match));

            return RedirectToAction("Index", "LeaderBoard", new { lastSequenceNumber = commandResult.GetNewPosition() });
        }
    }
}