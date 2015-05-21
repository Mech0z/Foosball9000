using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Exceptions;
using Common.Logging;
using FoosballOld.ActionFilters;
using FoosballOld.ContainerInstallers;

namespace Foosball9000Api
{
    public class WebApiApplication : HttpApplication
    {
        public IWindsorContainer Container { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            ContainerInstaller();
        }

        private void ContainerInstaller()
        {
            Container = new WindsorContainer();

            Container.Install(new MongoInstaller(), new LogicInstaller());

            Container.Register(
                Classes.FromAssemblyNamed("Common")
                    .BasedOn<ILogger>()
                    .WithServiceAllInterfaces()
                    .LifestylePerWebRequest());
            Container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            GlobalConfiguration.Configuration.Services.Replace(
                typeof (IHttpControllerActivator), new WindsorCompositionRoot(Container));

            GlobalConfiguration.Configuration.Services.Add(typeof (IExceptionLogger),
                new TraceExceptionLogger(new Logger()));
        }

        // ReSharper disable once RedundantOverridenMember
        public override void Dispose()
        {
            //TODO WHy is container null causing a null exception :s
            //Container.Dispose();
            base.Dispose();
        }

        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new TraceActionFilter(new Logger()));
        }

    }
}