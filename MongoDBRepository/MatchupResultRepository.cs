using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public interface IMatchupResultRepository
    {
        void SaveMatchupResult(MatchupResult matchupResult);
        List<MatchupResult> GetByHashResult(int hashcode);
    }

    public class MatchupResultRepository : MongoBase<MatchupResult>, IMatchupResultRepository
    {
        public MatchupResultRepository() : base("MatchupResults")
        {

        }

        public void SaveMatchupResult(MatchupResult matchupResult)
        {
            Collection.Save(matchupResult);
        }

        public List<MatchupResult> GetByHashResult(int hashcode)
        {
            return Collection.Find(Query<MatchupResult>.Where(x => x.HashResult == hashcode)).ToList();
        }
    }
}
