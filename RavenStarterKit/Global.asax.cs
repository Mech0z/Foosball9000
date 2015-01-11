using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Config;
using d60.Cirqus.MongoDb.Events;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.Serialization;
using d60.Cirqus.Views;
using MongoDB.Driver;
using MvcPWy.Cirqus;
using MvcPWy.Controllers;
using MvcPWy.PViews;

namespace MvcPWy
{
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            container.Register(Component.For<ICommandProcessorThing>().ImplementedBy<CommandProcessorThing>());
            var processor = container.Resolve<ICommandProcessorThing>();
        }

        protected void Application_End()
        {
            var processor = container.Resolve<ICommandProcessorThing>();
            processor.Processor.Dispose();
            container.Dispose();
        }
    }
}
