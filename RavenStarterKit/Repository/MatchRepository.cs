using System;
using System.Collections.Generic;
using System.Linq;
using Foosball9000.Models;
using MongoDB.Driver.Builders;

namespace MvcPWy.Repository
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

        public List<Match> Get10RecentMatches()
        {
            var sbb = new SortByBuilder();
            sbb.Descending(nameof(Match.TimeStampUtc));

            var matches = Collection.FindAllAs<Match>().SetSortOrder(sbb).SetLimit(10).ToList();

            return matches;
        }
    }
}