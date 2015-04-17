using Models;

namespace Logic
{
    public interface IMatchupHistoryCreator
    {
        void AddMatch(MatchV2 match);
        void RecalculateMatchupHistory();
    }
}