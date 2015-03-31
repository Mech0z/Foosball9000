using Models;

namespace Foosball.Logic
{
    public interface ICreateLeaderboardViewV2
    {
        LeaderboardView Get(bool recalculate);
    }
}