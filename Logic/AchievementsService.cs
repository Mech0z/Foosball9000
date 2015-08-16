using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDBRepository;

namespace Logic
{
    public class AchievementsService : IAchievementsService
    {
        public AchievementsService(ILeaderboardViewRepository leaderboardViewRepository,
            IMatchRepository matchRepository)
        {
            _leaderboardViewRepository = leaderboardViewRepository;
            _matchRepository = matchRepository;
        }

        private readonly ILeaderboardViewRepository _leaderboardViewRepository;
        private readonly IMatchRepository _matchRepository;
        
        public AchievementsView GetAchievementsView()
        {
            var leaderboardView = _leaderboardViewRepository.GetLeaderboardView();
            var matches = _matchRepository.GetMatches().OrderBy(m=>m.TimeStampUtc);

            int winStreak;
            LeaderboardViewEntry playerWin;

            GetStreak(matches, leaderboardView.Entries, true, out winStreak, out playerWin);

            int lossStreak;
            LeaderboardViewEntry playerLoss;

            GetStreak(matches, leaderboardView.Entries, false, out lossStreak, out playerLoss);

            var mostGames = leaderboardView.Entries.OrderByDescending(e => e.NumberOfGames).First();
            var mostWins = leaderboardView.Entries.OrderByDescending(e => e.Wins).First();
            var bestRatio = leaderboardView.Entries.OrderByDescending(e => e.Wins/e.NumberOfGames).First();


            var flawlessVictoryWinners = new List<string>();
            var flawlessVictoryLoosers = new List<string>();
            GetFlawlessVictory(matches, flawlessVictoryWinners, flawlessVictoryLoosers);
            
            var view = new AchievementsView
            {
                Achievements = new List<Achievement>()
                {
                    new Achievement()
                    {
                        Headline = "Most games",
                        UserName = mostGames.UserName,
                        Count = mostGames.NumberOfGames.ToString(),
                        Type = "Games"
                    },
                    new Achievement()
                    {
                        Headline = "Most wins",
                        UserName = mostWins.UserName,
                        Count = mostWins.Wins.ToString(),
                        Type = "Games"
                    },
                    new Achievement()
                    {
                        Headline = "Best win ratio",
                        UserName = bestRatio.UserName,
                        Count =
                            ((int) ((double) ((double) bestRatio.Wins/(double) bestRatio.NumberOfGames)*100)).ToString(),
                        Type = "Ratio"
                    },
                    new Achievement()
                    {
                        Headline = "Longest win streak",
                        UserName = playerWin.UserName,
                        Count = winStreak.ToString(),
                        Type = "Games"
                    },
                    new Achievement()
                    {
                        Headline = "Longest loss streak",
                        UserName = playerLoss.UserName,
                        Count = lossStreak.ToString(),
                        Type = "Games"
                    },
                    //new Achievement()
                    //{
                    //    Headline = "Most points for single match",
                    //    UserName = "Unknown",
                    //    Count = "",
                    //    Type = "Points"
                    //},
                    new Achievement()
                    {
                        Headline = "Flawless victory (10-0 win)",
                        UserName = "Winners: " + string.Join(" and ", flawlessVictoryWinners.ToArray()),
                        Count = string.Join(" and ", flawlessVictoryLoosers.ToArray()),
                        Type = "Loosers"
                    }
                }
            };

            return view;
        }

        private void GetStreak(IOrderedEnumerable<Match> matches, IEnumerable<LeaderboardViewEntry> entries, bool winStreak, out int streak, out LeaderboardViewEntry player)
        {
            player = null;
            int currentStreak = 0;
            streak = 0;

            foreach (var e in entries)
            {
                foreach (var match in matches)
                {
                    if (GetPlayers(match, winStreak).Contains(e.UserName))
                    {
                        currentStreak++;
                    }

                    if (GetPlayers(match, !winStreak).Contains(e.UserName))
                    {
                        if (currentStreak > streak)
                        {
                            streak = currentStreak;
                            player = e;
                        }

                        currentStreak = 0;
                    }
                }

                currentStreak = 0;
            }
        }

        private void GetFlawlessVictory(IOrderedEnumerable<Match> matches, List<string> winners, List<string> loosers)
        {
            var orderedMatches = matches.Where(m=>m.PlayerList.Count == 4).OrderByDescending(m => m.TimeStampUtc).ToList();
            foreach (var match in orderedMatches)
            {
                if(match.MatchResult.Team1Score == 0 )
                {
                    winners.AddRange(match.PlayerList.GetRange(2, 2));
                    loosers.AddRange(match.PlayerList.GetRange(0, 2));
                    return;
                }
                else if (match.MatchResult.Team2Score == 0)
                {
                    winners.AddRange(match.PlayerList.GetRange(0, 2));
                    loosers.AddRange(match.PlayerList.GetRange(2, 2));
                    return;
                }
            }
        }

        private string GetPlayers(Match m, bool winners)
        {
            if (m.MatchResult.Team1Won == winners)
            {
                return m.PlayerList[0] + ";" + m.PlayerList[1];
            }

            return m.PlayerList[2] + ";" + m.PlayerList[3];
        }
        
    }

    public interface IAchievementsService : ILogic
    {
        AchievementsView GetAchievementsView();
    }
}
