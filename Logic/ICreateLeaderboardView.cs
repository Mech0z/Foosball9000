using Models;

namespace Logic
{
    public interface ICreateLeaderboardView
    {
        LeaderboardView Get(bool recalculate);
    }
}