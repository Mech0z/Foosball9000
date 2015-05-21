using System.Configuration;
using System.Web.Mvc;

namespace Foosball9000Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            var version = ConfigurationManager.AppSettings["Version"];
            var environment = ConfigurationManager.AppSettings["Environment"];
            return string.Format("Foosball9000 API v{0} ({1})", version, environment);
        }
    }
}