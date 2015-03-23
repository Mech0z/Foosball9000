using System.Linq;
using Models;
using MongoDBRepository;
using System.Collections.Generic;

namespace Foosball.Logic
{
    public interface ICreateLeaderboardView
    {
        LeaderboardView Get(bool recalculate);
    }

    public class CreateLeaderboardView : ICreateLeaderboardView
    {
        private readonly ILeaderboardViewRepository _leaderboardViewRepository;
        private readonly IMatchRepository _matchRepository;

        public CreateLeaderboardView(ILeaderboardViewRepository leaderboardViewRepository, IMatchRepository matchRepository)
        {
            _leaderboardViewRepository = leaderboardViewRepository;
            _matchRepository = matchRepository;
        }

        public LeaderboardView Get(bool recalculate)
        {
            if (recalculate)
            {
                var matches = _matchRepository.GetMatches().OrderBy(x => x.TimeStampUtc);
                var leaderboardView = new LeaderboardView();

                foreach (var match in matches)
                {
                    var leaderboardEntries = leaderboardView.Entries;

                    //Team1
                    var player1 = match.Team1UserNames[0];
                    var existingPlayer1 = leaderboardEntries.SingleOrDefault(x => x.UserName == player1);
                    var player2 = match.Team1UserNames[1];
                    var existingPlayer2 = leaderboardEntries.SingleOrDefault(x => x.UserName == player2);

                    var team1AvgElo = existingPlayer1?.EloRating ?? 1500;
                    team1AvgElo += existingPlayer2?.EloRating ?? 1500;

                    //Team2
                    var player3 = match.Team2UserNames[0];
                    var existingPlayer3 = leaderboardEntries.SingleOrDefault(x => x.UserName == player3);
                    var player4 = match.Team2UserNames[1];
                    var existingPlayer4 = leaderboardEntries.SingleOrDefault(x => x.UserName == player4);

                    var team2AvgElo = existingPlayer3?.EloRating ?? 1500;
                    team2AvgElo += existingPlayer4?.EloRating ?? 1500;

                    var elo = new EloRating();
                    var result = elo.CalculateRating(team1AvgElo / 2, team2AvgElo / 2, match.MatchResults.Team1Won);

                    if (existingPlayer1 == null)
                    {
                        leaderboardEntries.Add(CreatePlayer(player1, match, result, match.MatchResults.Team1Won));
                    }
                    else
                    {
                        UpdateExistingLeaderboardEntry(existingPlayer1.UserName, leaderboardEntries, match, result, match.MatchResults.Team1Won);
                    }

                    if (existingPlayer2 == null)
                    {
                        leaderboardEntries.Add(CreatePlayer(player2, match, result, match.MatchResults.Team1Won));
                    }
                    else
                    {
                        UpdateExistingLeaderboardEntry(existingPlayer2.UserName, leaderboardEntries, match, result, match.MatchResults.Team1Won);
                    }

                    if (existingPlayer3 == null)
                    {
                        leaderboardEntries.Add(CreatePlayer(player3, match, result, !match.MatchResults.Team1Won));
                    }
                    else
                    {
                        UpdateExistingLeaderboardEntry(existingPlayer3.UserName, leaderboardEntries, match, result, !match.MatchResults.Team1Won);
                    }

                    if (existingPlayer4 == null)
                    {
                        leaderboardEntries.Add(CreatePlayer(player4, match, result, !match.MatchResults.Team1Won));
                    }
                    else
                    {
                        UpdateExistingLeaderboardEntry(existingPlayer4.UserName, leaderboardEntries, match, result, !match.MatchResults.Team1Won);
                    }
                }

                leaderboardView.Entries = leaderboardView.Entries.OrderByDescending(x => x.EloRating).ToList();

                _leaderboardViewRepository.SaveLeaderboardView(leaderboardView);
                return leaderboardView;
            }
            else
            {
                return _leaderboardViewRepository.GetLeaderboardView();
            }
        }

        public LeaderboardViewEntry CreatePlayer(string playerName, Match match, double result, bool won)
        {
            return new LeaderboardViewEntry
            {
                UserName = playerName,
                EloRating = won ? 1500 + (int)result : 1500 - (int)result,
                NumberOfGames = 1,
                Wins = won ? 1 : 0,
                Losses = won ? 0 : 1,
                Form = won ? "W" : "L"
            };
        }

        public void UpdateExistingLeaderboardEntry(string playerName, List<LeaderboardViewEntry> leaderboardEntries, Match match, double result, bool won)
        {
            var playerEntry = leaderboardEntries.Single(x => x.UserName == playerName);
            playerEntry.EloRating += won ? (int)result : - (int)result;
            playerEntry.NumberOfGames++;

            if (playerEntry.Form.Length < 5)
            {
                playerEntry.Form += won ? "W" : "L";
            }
            else
            {
                playerEntry.Form = playerEntry.Form.Remove(0, 1);
                playerEntry.Form += won ? "W" : "L";
            }

            if (won)
            {
                playerEntry.Wins++;
            }
            else
            {
                playerEntry.Losses++;
            }
        }
    }
}