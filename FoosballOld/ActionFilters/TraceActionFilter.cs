﻿using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Logging;

namespace FoosballOld.ActionFilters
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
                        _logger.Information("Request {Request} took {Elapsed}ms",
                            actionContext.Request.RequestUri,
                            stopWatch.ElapsedMilliseconds);
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