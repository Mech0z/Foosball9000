using System;
using System.Collections.Generic;
using System.Linq;
using Foosball.Models;
using Foosball.Repository;
using MongoDB.Driver.Builders;
using Microsoft.Framework.OptionsModel;

namespace Repository
{
    public class LeaderboardViewRepository : MongoBase<LeaderboardView>
    {
        public LeaderboardViewRepository(IOptions<Settings> settings) : base("LeaderboardViews", settings)
        {
            
        }
    }
}