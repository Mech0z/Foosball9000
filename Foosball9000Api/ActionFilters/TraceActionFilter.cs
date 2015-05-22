using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Logging;

namespace Foosball9000Api.ActionFilters
{
    public class TraceActionFilter : ActionFilterAttribute
    {
        private const string UnitOfWorkTimerKey = "UnitOfWorkTimer";
        private readonly ILogger _logger;

        public TraceActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.Properties[UnitOfWorkTimerKey] =
                new UnitOfWorkTimer(
                    (stopWatch =>
                    {
                        _logger.Information("Request {Request} took {Elapsed}ms. LocalPath {LocalPath}",
                            actionContext.Request.RequestUri,
                            stopWatch.ElapsedMilliseconds,
                            actionContext.Request.RequestUri.LocalPath);
                    }));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var unitOfWorkTimer = actionExecutedContext.Request.Properties[UnitOfWorkTimerKey] as UnitOfWorkTimer;
            if (unitOfWorkTimer != null)
            {
                unitOfWorkTimer.Dispose();
            }
        }
    }
}