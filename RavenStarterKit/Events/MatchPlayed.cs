using d60.Cirqus.Events;
using MvcPWy.AggRoots;

namespace MvcPWy.Events
{
    public class MatchPlayed : DomainEvent<DuelMatch>
    {
        public MatchPlayed(DuelMatch match)
        {
            Match = match;
        }

        public DuelMatch Match { get; set; }
    }
}