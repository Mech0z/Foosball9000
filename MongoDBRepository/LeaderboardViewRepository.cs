using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class LeaderboardViewRepository : MongoBase<LeaderboardView>, ILeaderboardViewRepository
    {
        public LeaderboardViewRepository() : base("LeaderboardViews")
        {

        }

        public LeaderboardView GetLeaderboardView(string seasonName)
        {
            return Collection.Find(Query<LeaderboardView>.Where(x => x.SeasonName == seasonName)).ToList().OrderByDescending(x => x.Timestamp).FirstOrDefault();
        }

        public List<LeaderboardView> GetLeaderboardViews()
        {
            return Collection.FindAll().OrderByDescending(x => x.Timestamp).ToList();
        }

        public void SaveLeaderboardView(LeaderboardView view)
        {
            Collection.Save(view);
        }
    }
}