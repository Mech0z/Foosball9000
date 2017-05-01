using System.Collections.Generic;
using Models;

namespace LogicTests
{
    public class MatchBuilder
    {
        private readonly Match result = new Match();

        public MatchBuilder SetPlayers(string p1, string p2, string p3, string p4)
        {
            result.PlayerList = new List<string>
            {
                p1,
                p2,
                p3,
                p4
            };
            return this;
        }

        public MatchBuilder SetResult(int team1Score, int team2Score)
        {
            result.MatchResult = new MatchResult
            {
                Team1Score = team1Score,
                Team2Score = team2Score
            };
            return this;
        }

        public Match Build()
        {
            return result;
        }
    }
}