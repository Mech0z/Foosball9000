﻿using System;
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

        public List<Match> GetMatches(string season)
        {
<<<<<<< HEAD
            return Collection.Find(Query<Match>.Where(x => x.SeasonName == "Season 2")).ToList();
=======
            if (season == null)
            {
                return Collection.FindAll().ToList();
            }

            return Collection.Find(Query<Match>.Where(x => x.SeasonName == season)).ToList();
>>>>>>> seasons
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