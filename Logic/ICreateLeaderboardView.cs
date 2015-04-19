using Models;

namespace Logic
{
    public interface ICreateLeaderboardView : ILogic
    {
        LeaderboardView Get(bool recalculate);
    }
}