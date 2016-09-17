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
            Collection.Save(season, WriteConcern.Acknowledged);
        }

        public void EndSeason(Season season)
        {
            var currentSeason = Collection.Find(Query<Season>.Where(x => x.Name == season.Name)).SingleOrDefault();

            currentSeason.EndDate = DateTime.UtcNow.Date.AddHours(23);
            Collection.Save(currentSeason, WriteConcern.Acknowledged);
        }
    }
}
