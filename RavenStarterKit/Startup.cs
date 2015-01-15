using Microsoft.Owin;
using MvcPWy;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace MvcPWy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}