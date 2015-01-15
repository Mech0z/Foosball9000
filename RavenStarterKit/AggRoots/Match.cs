using System.Collections.Generic;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;
using MvcPWy.Events;
using MvcPWy.Models;

namespace MvcPWy.AggRoots
{
    public class Match : AggregateRoot, IEmit<MatchPlayed>
    {
        public List<string> Team1UserNames { get; set; }
        public List<string> Team2UserNames { get; set; }

        public void SetMatch(Match match)
        {
            Emit(new MatchPlayed { Match = match});
        }
        /// <summary>
        /// True means first player in list is defence and second is offence
        /// </summary>
        public bool StaticFormationTeam1 { get; set; }
        public bool StaticFormationTeam2 { get; set; }

        public MatchResult MatchResults { get; set; }

        public void Apply(MatchPlayed e)
        {
            Team1UserNames = e.Match.Team1UserNames;
            Team2UserNames = e.Match.Team2UserNames;
            StaticFormationTeam1 = e.Match.StaticFormationTeam1;
            StaticFormationTeam2 = e.Match.StaticFormationTeam2;
            MatchResults = e.Match.MatchResults;
        }
    }
}