using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AchievementsView
    {
        public string MostWinsPlayer { get; set; }
        public int MostWinsCount { get; set; }

        public string MostGamesPlayer { get; set; }
        public int MostGamesCount { get; set; }

        public string BestRatioPlayer { get; set; }
        public int BestRatioPercent { get; set; }

        public string WinStreakPlayer { get; set; }
        public int WinStreakMatches { get; set; }

        public string LossStreakPlayer { get; set; }
        public int LossStreakMatches { get; set; }
    }
}
