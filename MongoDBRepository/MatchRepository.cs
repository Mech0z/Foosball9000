using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class MatchRepository : MongoBase<Match>, IMatchRepository
    {
        public MatchRepository() : base("Matches")
        {
            
        }

        public void SaveMatch(Match draft)
        {
            Collection.Save(draft);
        }

        public List<Match> GetMatches()
        {
            return Collection.FindAll().ToList();
        }

        public Match GetByTimeStamp(DateTime time)
        {
            return Collection.Find(Query<Match>.Where(x => x.TimeStampUtc == time)).SingleOrDefault();
        }

        public List<Match> GetRecentMatches(int numberOfMatches)
        {
            var sbb = new SortByBuilder();
            sbb.Descending("TimeStampUtc");

            var matches = Collection.FindAllAs<Match>().SetSortOrder(sbb).SetLimit(numberOfMatches).ToList();

            return matches;
        }
    }
}