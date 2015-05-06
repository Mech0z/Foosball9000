using System;
using System.Collections.Generic;

namespace Models
{
    public class Match
    {
        public Guid Id { get; set; }

        public DateTime TimeStampUtc { get; set; }

        /// <summary>
        /// Ordered:
        /// Team 1 Defence
        /// Team 1 Offence
        /// Team 2 Defence
        /// Team 2 Offence
        /// </summary>
        public List<string> PlayerList { get; set; }

        /// <summary>
        /// True means first player in list is defence and second is offence
        /// </summary>
        public bool StaticFormationTeam1 { get; set; }
        public bool StaticFormationTeam2 { get; set; }

        public MatchResult MatchResult { get; set; }
    }
}