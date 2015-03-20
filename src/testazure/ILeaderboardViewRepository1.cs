using Models;

namespace Repository
{
    public interface ILeaderboardViewRepository1
    {
        LeaderboardView GetLeaderboardView();
        void SaveLeaderboardView(LeaderboardView view);
    }
}