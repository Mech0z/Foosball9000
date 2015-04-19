using Models;

namespace Logic
{
    public interface IMatchupHistoryCreator : ILogic
    {
        void AddMatch(MatchV2 match);
        void RecalculateMatchupHistory();
    }
}