using System.Collections.Generic;
using Models;

namespace Logic
{
    public interface ISeasonLogic
    {
        string StartNewSeason();
        List<Season> GetSeasons();
        Season GetActiveSeason();
    }
}