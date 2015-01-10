using d60.Cirqus.Commands;
using MvcPWy.AggRoots;

namespace MvcPWy.Commands
{
    public class AddMatch : Command<Match>
    {
        private readonly Match _match;

        public AddMatch(string aggregateRootId, Match match) : base(aggregateRootId)
        {
            _match = match;
        }

        public override void Execute(Match aggregateRoot)
        {
            aggregateRoot.SetMatch(_match);
        }
    }
}