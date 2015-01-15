using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using d60.Cirqus;
using d60.Cirqus.Config;
using d60.Cirqus.MongoDb.Config;
using d60.Cirqus.Views;
using MvcPWy.Models;

namespace MvcPWy.Cirqus
{
    public class CommandProcessorThing : ICommandProcessorThing
    {
        private readonly IMongoInstance _mongoInstance;

        public ICommandProcessor Processor { get; set; }

        public CommandProcessorThing(IMongoInstance mongoInstance, IEnumerable<IViewManager> viewManagers)
        {
            _mongoInstance = mongoInstance;

            Processor = CommandProcessor.With()
                .EventStore(e => e.UseMongoDb(_mongoInstance.GetConnectionstring(), "Events"))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManagers.ToArray()))
                .Create();
        }
    }
}