using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using RestSharp;
using WebApplication1.Models;
using WebApplication1.Models.MigrationModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IActionResult Index()
        {
            CreatePlayers();

            var users = _context.Users.ToList();
            
            

            return View();
        }

        private void GetMatches(List<ApplicationUser> applicationUsers)
        {
            RestClient client = new RestClient("http://foosball9000api.sovs.net/api/");

            RestRequest restRequest2 = new RestRequest("/Match/GetAll", Method.GET);

            IRestResponse<List<Match>> response2 = client.Execute<List<Match>>(restRequest2);
            
            List<Match> mathces = response2.Data;

            List<FoosballMatch> foosballMatches = new List<FoosballMatch>();

            foreach (Match match in mathces)
            {
                FoosballMatch foosballMatch = new FoosballMatch();

                foosballMatch.TimeStampUtc = match.TimeStampUtc;

                foosballMatch.Team1Player1 = applicationUsers.SingleOrDefault(x => x.Email == match.PlayerList[0]);
                foosballMatch.Team1Player2 = applicationUsers.SingleOrDefault(x => x.Email == match.PlayerList[1]);
                foosballMatch.Team2Player1 = applicationUsers.SingleOrDefault(x => x.Email == match.PlayerList[2]);
                foosballMatch.Team2Player2 = applicationUsers.SingleOrDefault(x => x.Email == match.PlayerList[3]);

                foosballMatch.Points = match.Points;
                foosballMatch.Team1Score = match.MatchResult.Team1Score;
                foosballMatch.Team2Score = match.MatchResult.Team2Score;

                foosballMatches.Add(foosballMatch);
            }
        }

        private void CreatePlayers()
        {
            RestClient client = new RestClient("http://foosball9000api.sovs.net/api/");

            RestRequest restRequest = new RestRequest("/player/GetUsers", Method.GET);

            IRestResponse<List<FoosballUser>> response = client.Execute<List<FoosballUser>>(restRequest);

            List<FoosballUser> players = response.Data;

            CreateUeApplicationUsers(players);
        }

        public List<ApplicationUser> CreateUeApplicationUsers(List<FoosballUser> foosballUsers)
        {
            List<ApplicationUser> newUsers = new List<ApplicationUser>();

            foreach (FoosballUser foosballUser in foosballUsers)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = foosballUser.Email,
                    UserName = foosballUser.Username.Replace(" ", string.Empty),
                    Name = foosballUser.Username,
                    GravatarEmail = foosballUser.GravatarEmail
                };

                IdentityResult newUser = _userManager.CreateAsync(applicationUser, "P@ssw0rd").Result;

                if (newUser.Succeeded)
                {
                    newUsers.Add(applicationUser);
                }
                else
                {
                    Console.WriteLine(newUser.Errors.First().Description);
                }
            }

            return newUsers;
        } 
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
