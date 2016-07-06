using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Season
    {
        [BsonId]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
