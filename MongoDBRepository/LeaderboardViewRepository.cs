using System.Linq;
using Models;

namespace MongoDBRepository
{
    public class LeaderboardViewRepository : MongoBase<LeaderboardView>, ILeaderboardViewRepository
    {
        public LeaderboardViewRepository() : base("LeaderboardViews")
        {

        }

        public LeaderboardView GetLeaderboardView()
        {
            return Collection.FindAll().ToList().OrderByDescending(x => x.Timestamp).FirstOrDefault();
        }

        public void SaveLeaderboardView(LeaderboardView view)
        {
            Collection.Save(view);
        }
    }
}