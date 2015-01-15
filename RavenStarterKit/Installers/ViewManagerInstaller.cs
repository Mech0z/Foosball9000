using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.Views;
using d60.Cirqus.Views.ViewManagers;
using MvcPWy.Models;
using MvcPWy.PViews;

namespace MvcPWy.Installers
{
    public class ViewManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterMongoDbView<LeaderboardView>(container);

            //container.Register(
            //        Classes.FromThisAssembly().BasedOn<IViewManager>()
            //        .WithServiceAllInterfaces()
            //        .LifestyleSingleton()
                
        }

        private void RegisterMongoDbView<T>(IWindsorContainer container) where T : class, IViewInstance, ISubscribeTo, new()
        {
            container.Register(
                Component.For<IViewManager<T>>()
                    .UsingFactoryMethod(k => new MongoDbViewManager<T>(k.Resolve<IMongoInstance>().GetConnectionstring())),

                Component.For<IViewManager>()
                    .UsingFactoryMethod(k => k.Resolve<IViewManager<T>>())

                );
        }
    }
}