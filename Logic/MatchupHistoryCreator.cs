using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDBRepository;

namespace Logic
{
    public class MatchupHistoryCreator : IMatchupHistoryCreator
    {
        private readonly IMatchupResultRepository _matchupResultRepository;

        public MatchupHistoryCreator(IMatchupResultRepository matchupResultRepository)
        {
            _matchupResultRepository = matchupResultRepository;
        }

        public void AddMatch(MatchV2 match)
        {
            //Sort
            var sortedUserlist = match.PlayerList.OrderBy(x => x).ToList();
            var addedList = string.Join("", sortedUserlist.ToArray());

            //Get hashstring
            var hashcode = addedList.GetHashCode();

            //Find existing matchup historys that have the same players
            List<MatchupResult> matchingResults = _matchupResultRepository.GetByHashResult(hashcode);

            //Find the team constalation with same team setup
            var team1 = new List<string> { match.PlayerList[0], match.PlayerList[1] }.OrderBy(x => x).ToList();
            var team2 = new List<string> { match.PlayerList[2], match.PlayerList[3] }.OrderBy(x => x).ToList();

            MatchupResult correctMatchupResult = null;
            foreach (var matchResult in matchingResults)
            {
                var sortedFoundMatchResultTeam1 = new List<string> { matchResult.UserList[0], matchResult.UserList[1] }.OrderBy(x => x).ToList();
                var sortedFoundMatchResultTeam2 = new List<string> { matchResult.UserList[2], matchResult.UserList[3] }.OrderBy(x => x).ToList();

                if (team1[0] == sortedFoundMatchResultTeam1[0] && team1[1] == sortedFoundMatchResultTeam1[1])
                {
                    correctMatchupResult = matchResult;
                    break;
                } else if (team2[0] == sortedFoundMatchResultTeam2[0] && team2[1] == sortedFoundMatchResultTeam2[1])
                {
                    correctMatchupResult = matchResult;
                    break;
                }
            }
            
            //Add match
            if (correctMatchupResult != null)
            {
                AddMatchupResult(correctMatchupResult, match);
                _matchupResultRepository.SaveMatchupResult(correctMatchupResult);
            }
            else
            {
                //Create new matchcupresult
                var matchup = new MatchupResult
                {
                    HashResult = hashcode,
                    Last5Games = new List<MatchResult> {match.MatchResult},
                    Team1Wins = 0,
                    Team2Wins = 0,
                    UserList = match.PlayerList
                };
                _matchupResultRepository.SaveMatchupResult(matchup);
            }
        }

        public void RecalculateMatchupHistory()
        {
            throw new System.NotImplementedException();
        }

        public void AddMatchupResult(MatchupResult existingMatchupResult, MatchV2 match)
        {
            //Find out if team 1
            if (match.PlayerList[0] == existingMatchupResult.UserList[0] ||
                match.PlayerList[0] == existingMatchupResult.UserList[1])
            {
                if (match.MatchResult.Team1Won)
                {
                    existingMatchupResult.Team1Wins++;
                }
                else
                {
                    existingMatchupResult.Team2Wins++;
                }
            }
            else
            {
                if (match.MatchResult.Team1Won)
                {
                    existingMatchupResult.Team2Wins++;
                }
                else
                {
                    existingMatchupResult.Team1Wins++;
                }
            }

            if (existingMatchupResult.Last5Games.Count < 5)
            {
                //TODO Need to take care when team 1 of existing is not the same as team1 in match
                existingMatchupResult.Last5Games.Insert(0, match.MatchResult);
            }
            else
            {
                existingMatchupResult.Last5Games.RemoveAt(4);
                existingMatchupResult.Last5Games.Insert(0, match.MatchResult);
            }
        }
    }
}