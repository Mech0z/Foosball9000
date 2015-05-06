using Models;

namespace Logic
{
    public interface IMatchupHistoryCreator : ILogic
    {
        void AddMatch(Match match);
        void RecalculateMatchupHistory();
    }
}