using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDBRepository;

namespace Logic
{
    public class SeasonLogic : ISeasonLogic
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonLogic(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }
        
        public string StartNewSeason()
        {
            var seasons = _seasonRepository.GetSeasons();
            var newSeasonNumber = seasons.Count + 1;

            var activeSeason = seasons.SingleOrDefault(x => x.EndDate == null);
            
            if (activeSeason != null)
            {
                if (activeSeason.StartDate.Date.AddDays(-1).Date == DateTime.UtcNow.Date)
                {
                    throw new Exception("Cant start new season yet");
                }

                _seasonRepository.EndSeason(activeSeason);
            }

            var newSeason = new Season
            {
                StartDate = activeSeason != null ? DateTime.UtcNow.Date.AddDays(1) : DateTime.UtcNow.Date,
                Name = string.Format("Season {0}", newSeasonNumber)
            };

            _seasonRepository.CreateNewSeason(newSeason);

            return newSeason.Name;
        }

        public List<Season> GetSeasons()
        {
            return _seasonRepository.GetSeasons();
        }

        public Season GetActiveSeason()
        {
            var existingSeasons = _seasonRepository.GetSeasons();

            return existingSeasons.SingleOrDefault(x => x.EndDate == null);
        }
    }
}