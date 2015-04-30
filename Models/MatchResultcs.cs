namespace Models
{
    public class MatchResult
    {
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

        public bool Team1Won
        {
            get { return Team1Score > Team2Score; }
        }
    }
}