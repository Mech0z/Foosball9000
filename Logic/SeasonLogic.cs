﻿using System;
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

        public void ConvertOldMatches()
        {
            List<Match> season1Matches = _matchSeason1LegacyRepository.GetMatches();



            foreach (Match match in season1Matches)
            {
                match.SeasonName = seasons.Single(x => x.EndDate != null);
                _matchRepository.SaveMatch(match);
            }

            List<Match> season2Matches = _matchSeason2LegacyRepository.GetMatches();
            
            
        }
    }

    public interface ISeasonLogic
    {
        void ConvertOldMatches();
    }
}