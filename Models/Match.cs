using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Match
    {
        public Match()
        {
            IsThisRandom = true;
        }
        
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

        public bool IsThisRandom { get; set; }

        public int Team1HashCode
        {
            get
            {
                //TODO dont seem optimal to create a list every time
                var list = new List<string> { PlayerList[0], PlayerList[1] };
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public int Team2HashCode
        {
            get
            {
                var list = new List<string> { PlayerList[2], PlayerList[3] };
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public MatchResult MatchResult { get; set; }

        public int? Points { get; set; }

        public String SeasonName { get; set; }
    }
}