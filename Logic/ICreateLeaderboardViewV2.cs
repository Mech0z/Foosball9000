using Models;

namespace Logic
{
    public interface ICreateLeaderboardViewV2
    {
        LeaderboardView Get(bool recalculate);
    }
}