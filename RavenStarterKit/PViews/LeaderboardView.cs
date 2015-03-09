using System;
using System.Collections.Generic;
using System.Linq;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using MvcPWy.AggRoots;
using MvcPWy.Events;
using MvcPWy.Models;
using MvcPWy.Repository;

namespace MvcPWy.PViews
{
    public class LeaderboardView : IViewInstance<GlobalInstanceLocator>, ISubscribeTo<MatchPlayed>
    {
        public LeaderboardView()
        {
            LeaderboardEntries = new List<LeaderboardEntry>();
        }

        public List<LeaderboardEntry> LeaderboardEntries { get; set; }

        public void Handle(IViewContext context, MatchPlayed domainEvent)
        {
            


            var aggregateRootId = domainEvent.GetAggregateRootId();
            
            var match = context.Load<Match>(aggregateRootId);

            

            //Team1
            var player1 = match.Team1UserNames[0];
            var existingPlayer1 = LeaderboardEntries.SingleOrDefault(x => x.UserName == player1);
            var player2 = match.Team1UserNames[1];
            var existingPlayer2 = LeaderboardEntries.SingleOrDefault(x => x.UserName == player2);

            var team1AvgElo = existingPlayer1?.EloRating ?? 1500;
            team1AvgElo += existingPlayer2?.EloRating ?? 1500;

            //Team2
            var player3 = match.Team2UserNames[0];
            var existingPlayer3 = LeaderboardEntries.SingleOrDefault(x => x.UserName == player3);
            var player4 = match.Team2UserNames[1];
            var existingPlayer4 = LeaderboardEntries.SingleOrDefault(x => x.UserName == player4);

            var team2AvgElo = existingPlayer3?.EloRating ?? 1500;
            team2AvgElo += existingPlayer4?.EloRating ?? 1500;
            var mail = "madsskipper@gmail.com";
            if (player1 == mail || player2 == mail || player3 == mail || player4 == mail)
            {
                Console.WriteLine(domainEvent.GetSequenceNumber());
            }

            var elo = new EloRating();
            var result = elo.CalculateRating(team1AvgElo/2, team2AvgElo/2, match.MatchResults.Team1Won);

            if (existingPlayer1 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player1,
                        EloRating = match.MatchResults.Team1Won ? (int) result : -(int) result,
                        NumberOfGames = 1
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer1.UserName).EloRating +=
                    match.MatchResults.Team1Won? (int) result : -(int) result;
                LeaderboardEntries.Single(x => x.UserName == existingPlayer1.UserName).NumberOfGames++;
            }

            if (existingPlayer2 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player2,
                        EloRating = match.MatchResults.Team1Won ? (int) result : -(int) result,
                        NumberOfGames = 1
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer2.UserName).EloRating +=
                    match.MatchResults.Team1Won ? (int)result : -(int)result;
                LeaderboardEntries.Single(x => x.UserName == existingPlayer2.UserName).NumberOfGames++;
            }

            if (existingPlayer3 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player3,
                        EloRating = match.MatchResults.Team1Won ? -(int) result : (int) result,
                        NumberOfGames = 1
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer3.UserName).EloRating +=
                    match.MatchResults.Team1Won ? -(int)result : (int)result;
                LeaderboardEntries.Single(x => x.UserName == existingPlayer3.UserName).NumberOfGames++;
            }

            if (existingPlayer4 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player4,
                        EloRating = match.MatchResults.Team1Won ? -(int) result : (int) result,
                        NumberOfGames = 1,
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer4.UserName).EloRating +=
                    match.MatchResults.Team1Won ? -(int)result : (int)result;
                LeaderboardEntries.Single(x => x.UserName == existingPlayer4.UserName).NumberOfGames++;
            }

            var repository = new MatchRepository();
            repository.SaveMatch(new Foosball9000.Models.Match
            {
                Id = Guid.NewGuid(),
                MatchResults = match.MatchResults,
                StaticFormationTeam1 = match.StaticFormationTeam1,
                StaticFormationTeam2 = match.StaticFormationTeam2,
                Team1UserNames = match.Team1UserNames,
                Team2UserNames = match.Team2UserNames,
                TimeStampUtc = domainEvent.GetUtcTime()
            });
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }
    }
}