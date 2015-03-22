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
            return Collection.FindAll().ToList().First();
        }

        public void SaveLeaderboardView(LeaderboardView view)
        {
            Collection.Save(view);
        }
    }
}