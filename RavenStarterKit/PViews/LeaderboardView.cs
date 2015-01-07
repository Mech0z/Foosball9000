﻿using System.Collections.Generic;
using System.Linq;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using MvcPWy.Events;
using MvcPWy.Models;

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
            //Team1
            var player1 = domainEvent.Match.Team1[0];
            var existingPlayer1 = LeaderboardEntries.Single(x => x.UserName == player1.UserName);
            var player2 = domainEvent.Match.Team1[1];
            var existingPlayer2 = LeaderboardEntries.Single(x => x.UserName == player2.UserName);

            var team1AvgElo = existingPlayer1?.EloRating ?? 1500;
            team1AvgElo += existingPlayer2?.EloRating ?? 1500;

            //Team2
            var player3 = domainEvent.Match.Team1[1];
            var existingPlayer3 = LeaderboardEntries.Single(x => x.UserName == player3.UserName);
            var player4 = domainEvent.Match.Team1[1];
            var existingPlayer4 = LeaderboardEntries.Single(x => x.UserName == player4.UserName);

            var team2AvgElo = existingPlayer3?.EloRating ?? 1500;
            team2AvgElo += existingPlayer4?.EloRating ?? 1500;

            var elo = new EloRating();
            var result = elo.CalculateRating(team1AvgElo/2, team2AvgElo/2, domainEvent.Match.MatchResults.Team1Won());

            if (existingPlayer1 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player1.UserName,
                        EloRating = domainEvent.Match.MatchResults.Team1Won() ? (int) result : -(int) result
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer1.UserName).EloRating +=
                    domainEvent.Match.MatchResults.Team1Won() ? (int) result : -(int) result;
            }

            if (existingPlayer2 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player2.UserName,
                        EloRating = domainEvent.Match.MatchResults.Team1Won() ? (int) result : -(int) result
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer2.UserName).EloRating +=
                    domainEvent.Match.MatchResults.Team1Won() ? (int)result : -(int)result;
            }

            if (existingPlayer3 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player3.UserName,
                        EloRating = domainEvent.Match.MatchResults.Team1Won() ? -(int) result : (int) result
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer3.UserName).EloRating +=
                    domainEvent.Match.MatchResults.Team1Won() ? -(int)result : (int)result;
            }

            if (existingPlayer4 == null)
            {
                LeaderboardEntries.Add(
                    new LeaderboardEntry
                    {
                        UserName = player4.UserName,
                        EloRating = domainEvent.Match.MatchResults.Team1Won() ? -(int) result : (int) result
                    });
            }
            else
            {
                LeaderboardEntries.Single(x => x.UserName == existingPlayer4.UserName).EloRating +=
                    domainEvent.Match.MatchResults.Team1Won() ? -(int)result : (int)result;
            }
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }
    }
}