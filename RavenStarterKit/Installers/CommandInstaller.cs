using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using d60.Cirqus.Views;
using MvcPWy.Cirqus;
using MvcPWy.Models;

namespace MvcPWy.Installers
{
    public class CommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommandProcessorThing>()
                .UsingFactoryMethod(k =>
                {
                    var mongoInstance = k.Resolve<IMongoInstance>();
                    var viewManagers = k.ResolveAll<IViewManager>();
                    return new CommandProcessorThing(mongoInstance, viewManagers);
                }));
        }
    }
}