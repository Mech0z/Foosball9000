using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface ISeasonRepository
    {
        List<Season> GetSeasons();
        void CreateNewSeason(Season season);
        void EndSeason(Season season);
    }
}