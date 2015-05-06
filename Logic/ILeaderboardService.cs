using Models;

namespace Logic
{
    public interface ILeaderboardService : ILogic
    {
        LeaderboardView RecalculateLeaderboard();
        LeaderboardView GetLatestLeaderboardView();
        void AddMatchToLeaderboard(LeaderboardView leaderboardView, Match match);
    }
}