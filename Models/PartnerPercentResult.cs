namespace Models
{
    public class PartnerPercentResult
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public double? UsersNormalWinrate { get; set; }
        public int MatchesTogether { get; set; }
        public int WinsTogether { get; set; }
        public int MatchesAgainst { get; set; }
        public int WinsAgainst { get; set; }
    }
}
