using System.Collections.Generic;
using Models;

namespace Foosball9000Api.RequestResponse
{
    public class SaveMatchesRequest
    {
        public User User { get; set; }
        public List<Match> Matches { get; set; }
    }
}