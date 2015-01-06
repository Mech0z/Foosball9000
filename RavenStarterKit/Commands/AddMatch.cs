using d60.Cirqus.Commands;
using MvcPWy.AggRoots;
using MvcPWy.Models;

namespace MvcPWy.Commands
{
    public class AddMatch : Command<DuelMatch>
    {
        public AddMatch(string aggregateRootId) : base(aggregateRootId)
        {
        }

        public override void Execute(DuelMatch aggregateRoot)
        {
            aggregateRoot.AddMatch(aggregateRoot);
        }
    }
}