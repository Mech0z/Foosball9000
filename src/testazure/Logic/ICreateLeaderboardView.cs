using Models;

namespace Foosball.Logic
{
    public interface ICreateLeaderboardView
    {
        LeaderboardView Get(bool recalculate);
    }
}