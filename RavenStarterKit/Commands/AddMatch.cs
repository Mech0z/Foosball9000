using d60.Cirqus.Commands;
using MvcPWy.AggRoots;
using MvcPWy.Models;

namespace MvcPWy.Commands
{
    public class AddMatch : Command<Match>
    {
        private Match _match;

        public AddMatch(string aggregateRootId, Match match) : base(aggregateRootId)
        {
            _match = match;
        }

        public override void Execute(Match aggregateRoot)
        {
        }
    }
}