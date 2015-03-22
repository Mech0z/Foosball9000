using System.Linq;
using Models;
using MongoDBRepository;

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
                var matches = _matchRepository.GetMatches();
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
                        leaderboardEntries.Add(
                            new LeaderboardViewEntry
                            {
                                UserName = player1,
                                EloRating = match.MatchResults.Team1Won ? 1500 + (int)result : 1500 - (int)result,
                                NumberOfGames = 1,
                                Wins = match.MatchResults.Team1Won ? 1 : 0
                            });
                    }
                    else
                    {
                        var player1Entry = leaderboardEntries.Single(x => x.UserName == existingPlayer1.UserName);

                        player1Entry.EloRating +=
                            match.MatchResults.Team1Won ? (int)result : -(int)result;
                        player1Entry.NumberOfGames++;
                        if (match.MatchResults.Team1Won)
                        {
                            player1Entry.Wins++;
                        }
                        else
                        {
                            player1Entry.Losses++;
                        }
                    }

                    if (existingPlayer2 == null)
                    {
                        leaderboardEntries.Add(
                            new LeaderboardViewEntry
                            {
                                UserName = player2,
                                EloRating = match.MatchResults.Team1Won ? 1500 + (int)result : 1500 - (int)result,
                                NumberOfGames = 1,
                                Wins = match.MatchResults.Team1Won ? 1 : 0
                            });
                    }
                    else
                    {
                        var player2Entry = leaderboardEntries.Single(x => x.UserName == existingPlayer2.UserName);

                        player2Entry.EloRating +=
                            match.MatchResults.Team1Won ? (int)result : -(int)result;
                        player2Entry.NumberOfGames++;
                        if (match.MatchResults.Team1Won)
                        {
                            player2Entry.Wins++;
                        }
                        else
                        {
                            player2Entry.Losses++;
                        }
                    }

                    if (existingPlayer3 == null)
                    {
                        leaderboardEntries.Add(
                            new LeaderboardViewEntry
                            {
                                UserName = player3,
                                EloRating = match.MatchResults.Team1Won ? 1500 -(int)result : 1500 + (int)result,
                                NumberOfGames = 1,
                                Wins = match.MatchResults.Team1Won ? 0 : 1
                            });
                    }
                    else
                    {
                        var player3Entry = leaderboardEntries.Single(x => x.UserName == existingPlayer3.UserName);
                        
                        player3Entry.EloRating +=
                            match.MatchResults.Team1Won ? -(int)result : (int)result;
                        player3Entry.NumberOfGames++;
                        if (match.MatchResults.Team1Won)
                        {
                            player3Entry.Losses++;
                        }
                        else
                        {
                            player3Entry.Wins++;
                        }
                    }

                    if (existingPlayer4 == null)
                    {
                        leaderboardEntries.Add(
                            new LeaderboardViewEntry
                            {
                                UserName = player4,
                                EloRating = match.MatchResults.Team1Won ? 1500 - (int)result : 1500 + (int)result,
                                NumberOfGames = 1,
                                Wins = match.MatchResults.Team1Won ? 0 : 1
                            });
                    }
                    else
                    {
                        var player4Entry = leaderboardEntries.Single(x => x.UserName == existingPlayer4.UserName);
                        player4Entry.EloRating +=
                            match.MatchResults.Team1Won ? -(int)result : (int)result;
                        player4Entry.NumberOfGames++;
                        if (match.MatchResults.Team1Won)
                        {
                            player4Entry.Losses++;
                        }
                        else
                        {
                            player4Entry.Wins++;
                        }
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
    }
}