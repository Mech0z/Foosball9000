using System;
using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface IMatchRepository
    {
        void SaveMatch(Match match);
        List<Match> GetMatches(string season);
        Match GetByTimeStamp(DateTime dateTime);
        List<Match> GetRecentMatches(int numberOfMatches);
        List<Match> GetPlayerMatches(string email);
        IEnumerable<Match> GetMatchesByTimeStamp(DateTime time);
    }
}