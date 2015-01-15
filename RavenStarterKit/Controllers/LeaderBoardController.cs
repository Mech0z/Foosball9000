using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using d60.Cirqus;
using d60.Cirqus.Testing;
using d60.Cirqus.Views.ViewManagers;
using MvcPWy.PViews;

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
                await _leaderboardViewManager.WaitUntilProcessed(commandProcessingResult, new TimeSpan(0, 0, 5));
            }
            else
            {
                var commandProcessingResult = CommandProcessingResult.WithNewPosition(11);
                await _leaderboardViewManager.WaitUntilProcessed(commandProcessingResult, new TimeSpan(0, 0, 5));
            }
            
            var view = _leaderboardViewManager.Load("__global__");
            return View(view);
        }
    }
}