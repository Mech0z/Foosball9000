using System;
using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface IMatchRepositoryV2
    {
        void SaveMatch(MatchV2 draft);
        List<MatchV2> GetMatches();
        MatchV2 GetByTimeStamp(DateTime dateTime);
        List<MatchV2> GetRecentMatches(int numberOfMatches);
        List<MatchV2> GetPlayerMatches(string email);
    }
}