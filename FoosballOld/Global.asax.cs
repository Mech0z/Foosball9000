﻿using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Exceptions;
using Common.Logging;
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

            Container.Install(new MongoInstaller(), new LogicInstaller());

            Container.Register(Classes.FromAssemblyNamed("Common").BasedOn<ILogger>().WithServiceAllInterfaces().LifestylePerWebRequest());
            Container.Register(Classes.FromAssemblyNamed("Common").BasedOn<ExceptionLogger>().WithServiceAllInterfaces().LifestylePerWebRequest());
            Container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new WindsorCompositionRoot(Container));

            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger(new Logger()));
        }

        public override void Dispose()
        {
            //TODO WHy is container null causing a null exception :s
            //Container.Dispose();
            base.Dispose();
        }
    }
}
