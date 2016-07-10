using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDBRepository;

namespace Logic
{
    public class SeasonLogic : ISeasonLogic
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchSeason1LegacyRepository _matchSeason1LegacyRepository;
        private readonly IMatchSeason2LegacyRepository _matchSeason2LegacyRepository;
        private readonly ISeasonRepository _seasonRepository;

        public SeasonLogic(IMatchRepository matchRepository, IMatchSeason1LegacyRepository matchSeason1LegacyRepository, IMatchSeason2LegacyRepository matchSeason2LegacyRepository, ISeasonRepository seasonRepository)
        {
            _matchRepository = matchRepository;
            _matchSeason1LegacyRepository = matchSeason1LegacyRepository;
            _matchSeason2LegacyRepository = matchSeason2LegacyRepository;
            _seasonRepository = seasonRepository;
        }

        public void StartNewSeason(string name)
        {
            var newSeason = new Season
            {
                StartDate = DateTime.Today,
                Name = name
            };

            _seasonRepository.CreateNewSeason(newSeason);
        }

        public void CreateSeasons()
        {
            _seasonRepository.CreateNewSeason(new Season
            {
                StartDate = new DateTime(2015, 2, 10),
                EndDate = new DateTime(2016, 1, 4),
                Name = "Season 1"
            });

            _seasonRepository.CreateNewSeason(new Season
            {
                StartDate = new DateTime(2016, 1, 4),
                Name = "Season 2"
            });
        }

        public List<Season> GetSeasons()
        {
            return _seasonRepository.GetSeasons();
        }

        public void ConvertOldMatches()
        {
            List<Match> season1Matches = _matchSeason1LegacyRepository.GetMatches();
            var count1 = season1Matches.Count;
            foreach (Match match in season1Matches)
            {
                match.SeasonName = "Season 1";
                _matchRepository.SaveMatch(match);
            }

            List<Match> season2Matches = _matchSeason2LegacyRepository.GetMatches();
            var count2 = season2Matches.Count;

            var count3 = season1Matches.Count + season2Matches.Count;
            foreach (Match match in season2Matches)
            {
                match.SeasonName = "Season 2";
                _matchRepository.SaveMatch(match);
            }


        }
    }

    public interface ISeasonLogic
    {
        void ConvertOldMatches();
        void CreateSeasons();
        List<Season> GetSeasons();  
    }
}
