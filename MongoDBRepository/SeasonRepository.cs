using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class SeasonRepository : MongoBase<Season>, ISeasonRepository
    {
        public SeasonRepository() : base("Seasons")
        {           

        }

        public List<Season> GetSeasons()
        {
            return Collection.FindAll().ToList();
        }

        public void CreateNewSeason(Season season)
        {
            Collection.Save(season, WriteConcern.Unacknowledged);
        }

        public void EndSeason(Season season)
        {
            var currentSeason = Collection.Find(Query<Season>.Where(x => x.Id == season.Id)).SingleOrDefault();

            currentSeason.EndDate = DateTime.Today;
            Collection.Save(currentSeason, WriteConcern.Unacknowledged);
        }
    }
}
