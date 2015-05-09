using System.Web.Http.ExceptionHandling;
using Common.Logging;

namespace Common.Exceptions
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        private readonly ILogger _logger;

        public TraceExceptionLogger(ILogger logger)
        {
            _logger = logger;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(context.Exception, "Unhandled exception: {ExceptionContext}", context.ExceptionContext);
        }
    }
}