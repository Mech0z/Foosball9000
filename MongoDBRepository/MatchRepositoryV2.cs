using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class MatchRepositoryV2 : MongoBase<MatchV2>, IMatchRepositoryV2
    {
        public MatchRepositoryV2() : base("MatchesV2")
        {
            
        }

        public void SaveMatch(MatchV2 draft)
        {
            Collection.Save(draft);
        }

        public List<MatchV2> GetMatches()
        {
            return Collection.FindAll().ToList();
        }

        public MatchV2 GetByTimeStamp(DateTime time)
        {
            return Collection.Find(Query<MatchV2>.Where(x => x.TimeStampUtc == time)).SingleOrDefault();
        }

        public List<MatchV2> GetRecentMatches(int numberOfMatches)
        {
            var sbb = new SortByBuilder();
            sbb.Descending("TimeStampUtc");

            var matches = Collection.FindAllAs<MatchV2>().SetSortOrder(sbb).SetLimit(numberOfMatches).ToList();

            return matches;
        }

        public List<MatchV2> GetPlayerMatches(string email)
        {
            return Collection.Find(Query<MatchV2>.Where(x => x.PlayerList.Contains(email))).ToList();
        }
    }
}