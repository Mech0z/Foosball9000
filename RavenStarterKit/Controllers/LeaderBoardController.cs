using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using d60.Cirqus;
using d60.Cirqus.Testing;
using d60.Cirqus.Views.ViewManagers;
using MvcPWy.PViews;
using MvcPWy.Repository;

namespace MvcPWy.Controllers
{
    public class LeaderBoardController : Controller
    {
        private readonly IViewManager<LeaderboardView> _leaderboardViewManager;

        public LeaderBoardController(IViewManager<LeaderboardView> leaderboardViewManager)
        {
            _leaderboardViewManager = leaderboardViewManager;
        }

        // GET: LeaderBoard
        public async Task<ActionResult> Index(int? lastSequenceNumber)
        {

            if (lastSequenceNumber != null)
            {
                var commandProcessingResult = CommandProcessingResult.WithNewPosition(lastSequenceNumber.Value);
                await _leaderboardViewManager.WaitUntilProcessed(commandProcessingResult, new TimeSpan(0, 0, 30));
            }
            else
            {
                var commandProcessingResult = CommandProcessingResult.WithNewPosition(99);
                await _leaderboardViewManager.WaitUntilProcessed(commandProcessingResult, new TimeSpan(0, 0, 30));
            }
            
            var view = _leaderboardViewManager.Load("__global__");
            return View(view);
        }

        public ActionResult LastGames()
        {
            var rep = new MatchRepository();

            var matches = rep.Get10RecentMatches();

            return View(matches);
        }
    }
}