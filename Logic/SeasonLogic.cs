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
                if (activeSeason.StartDate.AddDays(-1).Date >= DateTime.Now.Date)
                {
                    throw new Exception("Cant start new season yet");
                }

                _seasonRepository.EndSeason(activeSeason);
            }

            var newSeason = new Season
            {
                StartDate = activeSeason != null ? DateTime.Today.AddDays(1) : DateTime.Today,
                Name = $"Season {newSeasonNumber}"
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