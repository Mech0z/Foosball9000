namespace Models
{
    public class PartnerPercentResult
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public double? UsersNormalWinrate { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
    }
}
