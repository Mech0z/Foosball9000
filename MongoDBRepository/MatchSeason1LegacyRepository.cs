using System.Collections.Generic;
using System.Linq;
using Models;

namespace MongoDBRepository
{
    public class MatchSeason1LegacyRepository : MongoBase<Match>, IMatchSeason1LegacyRepository
    {
        public MatchSeason1LegacyRepository() : base("MatchesV2_Season1")
        {

        }

        public List<Match> GetMatches()
        {
            return Collection.FindAll().ToList();
        }
    }

    public interface IMatchSeason1LegacyRepository
    {
        List<Match> GetMatches();
    }

    public class MatchSeason2LegacyRepository : MongoBase<Match>, IMatchSeason2LegacyRepository
    {
        public MatchSeason2LegacyRepository() : base("MatchesV2_Season2")
        {
        }

        public List<Match> GetMatches()
        {
            return Collection.FindAll().ToList();
        }
    }

    public interface IMatchSeason2LegacyRepository
    {
        List<Match> GetMatches();
    }
}
