using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MigrationModels
{
    public class LeaderboardView
    {
        public Guid Id { get; set; }
        public List<LeaderboardViewEntry> Entries { get; set; }

        public DateTime? Timestamp { get; set; }

        public LeaderboardView()
        {
            Id = Guid.NewGuid();
            Entries = new List<LeaderboardViewEntry>();
            Timestamp = DateTime.UtcNow;
        }
    }
}