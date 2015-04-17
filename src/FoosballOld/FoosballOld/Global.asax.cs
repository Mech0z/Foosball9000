using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FoosballOld.ContainerInstallers;
using MongoDBRepository;

namespace FoosballOld
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

            ContainerInstaller();
        }

        private void ContainerInstaller()
        {
            Container = new WindsorContainer();

            Container.Install(new MongoInstaller());
            Container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());


            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new WindsorCompositionRoot(Container));
        }

        public override void Dispose()
        {
            Container.Dispose();
            base.Dispose();
        }
    }
}
