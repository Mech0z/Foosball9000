using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface ILeaderboardViewRepository
    {
        LeaderboardView GetLeaderboardView(string seasonName);
        List<LeaderboardView> GetLeaderboardViews();
        void SaveLeaderboardView(LeaderboardView view);
    }
}