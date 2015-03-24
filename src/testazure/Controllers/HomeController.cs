using Microsoft.AspNet.Mvc;

namespace Foosball.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IMatchRepository _matchRepository;
        //private readonly ICreateLeaderboardViewV2 _leaderboardViewV2;
        //private readonly IMatchRepositoryV2 _matchRepositoryV2;

        //public HomeController(IMatchRepository matchRepository, ICreateLeaderboardViewV2 leaderboardViewV2, IMatchRepositoryV2 matchRepositoryV2)
        //{
        //    _matchRepository = matchRepository;
        //    _leaderboardViewV2 = leaderboardViewV2;
        //    _matchRepositoryV2 = matchRepositoryV2;

        //    var originalMatches = _matchRepository.GetMatches();

        //    foreach (var match in originalMatches)
        //    {
        //        match.Team1UserNames.AddRange(match.Team2UserNames);
        //        var matchv2 = new MatchV2
        //        {
        //            Id = match.Id,
        //            MatchResults = match.MatchResults,
        //            PlayerList = match.Team1UserNames,
        //            StaticFormationTeam1 = !match.StaticFormationTeam1,
        //            StaticFormationTeam2 = !match.StaticFormationTeam2,
        //            TimeStampUtc = match.TimeStampUtc
        //        };

        //        _matchRepositoryV2.SaveMatch(matchv2);
        //    }
        //}


        public ViewResult Index()
        {
            ViewBag.Message = "Your application start page.";

            return View();
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