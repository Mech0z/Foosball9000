﻿using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Foosball9000Api.ContainerInstallers;

namespace Foosball9000Api
{
    public class WebApiApplication : HttpApplication
    {
        public IWindsorContainer Container { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContainerInstaller();
        }

        private void ContainerInstaller()
        {
            Container = new WindsorContainer();

            Container.Install(new MongoInstaller(), new LogicInstaller());
            
            Container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            GlobalConfiguration.Configuration.Services.Replace(
                typeof (IHttpControllerActivator), new WindsorCompositionRoot(Container));
        }

        // ReSharper disable once RedundantOverridenMember
        public override void Dispose()
        {
            //TODO WHy is container null causing a null exception :s
            //Container.Dispose();
            base.Dispose();
        }
    }
}