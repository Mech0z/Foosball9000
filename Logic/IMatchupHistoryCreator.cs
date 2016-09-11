using System;
using System.Collections.Generic;
using Models;

namespace Logic
{
    public interface IMatchupHistoryCreator : ILogic
    {
        List<PartnerPercentResult> GetPartnerWinPercent(string email, string season);
        void AddMatch(Match match);
        void RecalculateMatchupHistory();
    }
}