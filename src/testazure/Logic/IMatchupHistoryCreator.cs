using Models;

namespace Foosball.Logic
{
    public interface IMatchupHistoryCreator
    {
        void AddMatch(MatchV2 match);
        void RecalculateMatchupHistory();
    }
}