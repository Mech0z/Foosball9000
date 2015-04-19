using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Logic;

namespace FoosballOld.ContainerInstallers
{
    public class LogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<ILogic>()
                    .Where(Component.IsInSameNamespaceAs<ILogic>())
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()
                );
        }
    }
}