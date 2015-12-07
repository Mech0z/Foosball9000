using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class MatchupResult
    {
        public int HashResult { get; set; }
        public List<string> UserList { get; set; }

        public int Team1HashCode
        {
            get
            {
                //TODO dont seem optimal to create a list every time
                var list = new List<string> {UserList[0], UserList[1]};
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public int Team2HashCode
        {
            get
            {
                var list = new List<string> { UserList[2], UserList[3] };
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public int Team1Wins { get; set; }
        public int Team2Wins { get; set; }

        public List<MatchResult> Last5Games { get; set; }
    }
}