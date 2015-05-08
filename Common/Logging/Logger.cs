using System;
using System.Configuration;
using Serilog;
using Serilog.Events;

namespace Common.Logging
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _log;

        public Logger()
        {
            var version = ConfigurationManager.AppSettings["Version"];
            var environment = ConfigurationManager.AppSettings["Environment"];

            try
            {
                _log = new LoggerConfiguration()
                        .Enrich.WithProperty("Version",version)
                        .Enrich.WithProperty("Environment",environment)
                        .Enrich.WithProperty("Application","FoosballApi")
                        .WriteTo.Loggly()
                        .WriteTo.File(@"c:\temp\foosball9000.log",LogEventLevel.Information)
                        .MinimumLevel.Debug()
                        .CreateLogger();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _log.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _log.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _log.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _log.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Error(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            _log.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}