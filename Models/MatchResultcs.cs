namespace Models
{
    public class MatchResult
    {
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

        public bool Team1Won => Team1Score > Team2Score;
    }
}