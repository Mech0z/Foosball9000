using System;
using System.Collections.Generic;

namespace Models
{
    public class LeaderboardView
    {
        public Guid Id { get; set; }
        public List<LeaderboardViewEntry> Entries { get; set; }
    }
}