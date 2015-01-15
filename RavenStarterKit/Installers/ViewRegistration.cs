using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.Views.ViewManagers;
using MvcPWy.Models;
using MvcPWy.PViews;

namespace MvcPWy.Installers
{
    //public class ViewRegistration : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        var mongoInstance = container.Resolve<IMongoInstance>();
    //        var leaderboardViewManager = new MongoDbViewManager<LeaderboardView>(mongoInstance.GetDatabase(), "LeaderboardView");
    //        container.Register(
    //            Component.For<IViewManager<LeaderboardView>>()
    //                .Instance(leaderboardViewManager));
    //    }
    //}
}