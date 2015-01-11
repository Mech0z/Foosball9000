using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcPWy.Models
{
    public class MatchViewModel
    {
        public IEnumerable<SelectListItem> Usernames { get; set; }

        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Player3 { get; set; }
        public string Player4 { get; set; }

        public bool StaticFormation { get; set; }
        public DateTime Timestamp { get; set; }
        public MatchResult MatchResults { get; set; }
    }
}