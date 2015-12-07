using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class FoosballMatch
    {
        public long ID { get; set; }

        public DateTime TimeStampUtc { get; set; }
        
        public ApplicationUser Team1Player1 { get; set; }
        public ApplicationUser Team2Player1 { get; set; }
        public ApplicationUser Team1Player2 { get; set; }
        public ApplicationUser Team2Player2 { get; set; }
        
        public int Team1HashCode
        {
            get
            {
                //TODO dont seem optimal to create a list every time
                var list = new List<string> { Team1Player1.UserName, Team1Player2.UserName };
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public int Team2HashCode
        {
            get
            {
                var list = new List<string> { Team2Player1.UserName, Team2Player2.UserName };
                return list.OrderBy(x => x).GetHashCode();
            }
        }

        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

        public bool Team1Won => Team1Score > Team2Score;

        public int? Points { get; set; }
    }
}
