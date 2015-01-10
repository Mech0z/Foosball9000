using System.Collections.Generic;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;
using MvcPWy.Events;
using MvcPWy.Models;

namespace MvcPWy.AggRoots
{
    public class Match : AggregateRoot, IEmit<MatchPlayed>
    {
        public List<ApplicationUser> Team1 { get; set; }
        public List<ApplicationUser> Team2 { get; set; }

        public void SetMatch(Match match)
        {
            Emit(new MatchPlayed { Match = match});
        }
        /// <summary>
        /// True means first player in list is defence and second is offence
        /// </summary>
        public bool StaticFormation { get; set; }

        public MatchResult MatchResults { get; set; }

        public void Apply(MatchPlayed e)
        {
            Team1 = e.Match.Team1;
            Team2 = e.Match.Team2;
            StaticFormation = e.Match.StaticFormation;
            MatchResults = e.Match.MatchResults;
        }
    }
}