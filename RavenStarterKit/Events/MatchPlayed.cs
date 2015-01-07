using d60.Cirqus.Events;
using MvcPWy.AggRoots;

namespace MvcPWy.Events
{
    public class MatchPlayed : DomainEvent<Match>
    {
        public MatchPlayed(Match match)
        {
            Match = match;
        }

        public Match Match { get; set; }
    }
}