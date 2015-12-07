using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AchievementsView
    {
        public List<Achievement> Achievements { get; set; }
    }

    public class Achievement
    {
        public string Headline { get; set; }
        public string UserName { get; set; }
        public string Count { get; set; }
        public string Type { get; set; }
    }
}
