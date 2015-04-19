using Models;

namespace Logic
{
    public interface ICreateLeaderboardViewV2 : ILogic
    {
        LeaderboardView Get(bool recalculate);
    }
}