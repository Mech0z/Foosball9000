using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDBRepository;

namespace Foosball.Logic
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

            var matchResultfound = false;
            foreach (var matchResult in matchingResults)
            {
                var sortedFoundMatchResultTeam1 = new List<string> { matchResult.UserList[0], matchResult.UserList[1] }.OrderBy(x => x).ToList();
                var sortedFoundMatchResultTeam2 = new List<string> { matchResult.UserList[2], matchResult.UserList[3] }.OrderBy(x => x).ToList();

                if (team1[0] == sortedFoundMatchResultTeam1[0] && team1[1] == sortedFoundMatchResultTeam1[1])
                {
                    matchResultfound = true;
                    break;
                } else if (team2[0] == sortedFoundMatchResultTeam2[0] && team2[1] == sortedFoundMatchResultTeam2[1])
                {
                    matchResultfound = true;
                    break;
                }
            }
            
            //Add match
            if (!matchResultfound)
            {
                
            }

            throw new System.NotImplementedException();
        }

        public void RecalculateMatchupHistory()
        {
            throw new System.NotImplementedException();
        }
    }
}