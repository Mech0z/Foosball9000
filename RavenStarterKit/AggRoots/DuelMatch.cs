using System.Collections.Generic;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;
using MvcPWy.Events;
using MvcPWy.Models;

namespace MvcPWy.AggRoots
{
    public class DuelMatch : AggregateRoot, IEmit<MatchPlayed>
    {
        public List<ApplicationUser> Team1 { get; set; }
        public List<ApplicationUser> Team2 { get; set; }

        /// <summary>
        /// True means first player in list is defence and second is offence
        /// </summary>
        public bool StaticFormation { get; set; }

        public MatchResult MatchResults { get; set; }

        public void Apply(MatchPlayed e)
        {
            //Get current ratings
            var eloP1 = 1500;
            var eloP2 = 1500;

            var elo = new EloRating();
            var result = elo.CalculateRating(eloP1, eloP2, MatchResults.Team1Won());
        }

        public void AddMatch(DuelMatch duelMatch)
        {
            Emit(new MatchPlayed(duelMatch));
        }
    }
}