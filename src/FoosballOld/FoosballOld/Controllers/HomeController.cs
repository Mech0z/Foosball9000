using System.Web.Mvc;
using Logic;
using Models;
using MongoDBRepository;

namespace FoosballOld.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ICreateLeaderboardViewV2 _leaderboardViewV2;
        private readonly IMatchRepositoryV2 _matchRepositoryV2;

        public HomeController(IMatchRepository matchRepository, ICreateLeaderboardViewV2 leaderboardViewV2, IMatchRepositoryV2 matchRepositoryV2)
        {
           

            _matchRepository = matchRepository;
            _leaderboardViewV2 = leaderboardViewV2;
            _matchRepositoryV2 = matchRepositoryV2;

            var originalMatches = _matchRepository.GetMatches();

            foreach (var match in originalMatches)
            {
                match.Team1UserNames.AddRange(match.Team2UserNames);
                var matchv2 = new MatchV2
                {
                    Id = match.Id,
                    MatchResult = match.MatchResults,
                    PlayerList = match.Team1UserNames,
                    StaticFormationTeam1 = !match.StaticFormationTeam1,
                    StaticFormationTeam2 = !match.StaticFormationTeam2,
                    TimeStampUtc = match.TimeStampUtc
                };

                _matchRepositoryV2.SaveMatch(matchv2);
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
