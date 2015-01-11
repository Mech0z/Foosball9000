namespace MvcPWy.Models
{
    public class LeaderboardEntry
    {
        public int NumberOfGames { get; set; }
        public string UserName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int EloRating { get; set; }
    }
}