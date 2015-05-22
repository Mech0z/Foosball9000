using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MongoDBRepository;

namespace Foosball9000Api.ContainerInstallers
{
    public class MongoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<IMongo>()
                    .Where(Component.IsInSameNamespaceAs<IMongo>())
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient()
                );
        }
    }
}