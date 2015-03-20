using Models;

namespace MongoDBRepository
{
    public interface ILeaderboardViewRepository
    {
        LeaderboardView GetLeaderboardView();
        void SaveLeaderboardView(LeaderboardView view);
    }
}