using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcPWy.Models;

namespace MvcPWy.Installers
{
    public class MongoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var mongoInstance = new MongoInstance();
            container.Register(
                Component.For<IMongoInstance>()
                    .Instance(mongoInstance));
        }
    }
}