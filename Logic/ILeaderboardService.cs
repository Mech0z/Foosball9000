using System.Collections.Generic;
using Models;

namespace Logic
{
    public interface ILeaderboardService : ILogic
    {
        LeaderboardView RecalculateLeaderboard(string season);
        LeaderboardView GetActiveLeaderboard();
        List<LeaderboardView> GetLatestLeaderboardViews();
        void AddMatchToLeaderboard(LeaderboardView leaderboardView, Match match);
    }
}