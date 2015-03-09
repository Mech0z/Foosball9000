using System;
using System.Collections.Generic;
using Foosball9000.Models;

namespace MvcPWy.Repository
{
    public interface IMatchRepository
    {
        void SaveMatch(Match draft);
        List<Match> GetMatches();
        Match GetByTimeStamp(DateTime dateTime);
        List<Match> Get10RecentMatches();
    }
}