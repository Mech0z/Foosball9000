using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class MatchRepository : MongoBase<Match>, IMatchRepository
    {
        public MatchRepository() : base("MatchesV3")
        {

        }

        public void SaveMatch(Match match)
        {
            Collection.Save(match, WriteConcern.Unacknowledged);
        }

        public List<Match> GetMatches()
        {
            return Collection.FindAll().ToList();
        }

        public Match GetByTimeStamp(DateTime time)
        {
            return Collection.Find(Query<Match>.Where(x => x.TimeStampUtc == time)).SingleOrDefault();
        }

        public IEnumerable<Match> GetMatchesByTimeStamp(DateTime time)
        {
            return Collection.Find(Query<Match>.Where(x => x.TimeStampUtc >= time)).ToList();
        }


        public List<Match> GetRecentMatches(int numberOfMatches)
        {
            var sbb = new SortByBuilder();
            sbb.Descending("TimeStampUtc");

            var matches = Collection.FindAllAs<Match>().SetSortOrder(sbb).SetLimit(numberOfMatches).ToList();

            return matches;
        }

        public List<Match> GetPlayerMatches(string email)
        {
            return Collection.Find(Query<Match>.Where(x => x.PlayerList.Contains(email))).ToList();
        }
    }
}