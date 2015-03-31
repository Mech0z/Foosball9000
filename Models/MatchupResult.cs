using System.Collections.Generic;

namespace Models
{
    public class MatchupResult
    {
        public int HashResult { get; set; }
        public List<string> UserList { get; set; }
        
        public int Team1Wins { get; set; }
        public int Team2Wins { get; set; }

        public List<MatchResult> Last5Games { get; set; }
    }
}