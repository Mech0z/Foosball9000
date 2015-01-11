using d60.Cirqus;

namespace MvcPWy.Cirqus
{
    public interface ICommandProcessorThing
    {
        ICommandProcessor Processor { get; set; }
    }
}