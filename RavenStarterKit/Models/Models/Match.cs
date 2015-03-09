using System;
using System.Collections.Generic;
using MvcPWy.Models;

namespace Foosball9000.Models
{
    public class Match
    {
        public Guid Id { get; set; }

        public DateTime TimeStampUtc { get; set; }

        public List<string> Team1UserNames { get; set; }
        public List<string> Team2UserNames { get; set; }

        /// <summary>
        /// True means first player in list is defence and second is offence
        /// </summary>
        public bool StaticFormationTeam1 { get; set; }
        public bool StaticFormationTeam2 { get; set; }

        public MatchResult MatchResults { get; set; }
    }
}