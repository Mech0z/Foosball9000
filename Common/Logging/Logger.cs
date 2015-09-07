using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.LogReceiver;


namespace Common.Logging
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _log;

        public Logger()
        {
            var version = ConfigurationManager.AppSettings["Version"];
            var environment = ConfigurationManager.AppSettings["Environment"];
            var elasticsearchEndpointValue = ConfigurationManager.AppSettings["ElasticsearchEndpoint"];

            try
            {
                _log = new LoggerConfiguration()
                        .Enrich.WithProperty("Version",version)
                        .Enrich.WithProperty("Environment",environment)
                        .Enrich.WithProperty("Application","FoosballApi")
                        /*.WriteTo.Elasticsearch(
                                new ElasticsearchSinkOptions(new Uri(elasticsearchEndpointValue))
                                {
                                    AutoRegisterTemplate = true,
                                    BufferBaseFilename = @"c:\temp\logs\elasticsearchbuffer"
                                })*/
                        .WriteTo.LogReceiver()                        
                        .MinimumLevel.Debug()
                        .CreateLogger();
            }
            catch (Exception)
            {
                //ignore
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


        public ITaskTimer StartTask(string name)
        {
            return StartTask(name, null);
        }

        public ITaskTimer StartTask(string name, ITaskTimer parentTaskTimer)
        {
            var taskTimer = new TaskTimer
            {
                Id = Guid.NewGuid(),
                Name = name,
                StartedTime = DateTime.Now,
                Disposer = DisposeTaskTimer,
                Parent = parentTaskTimer,
            };

            if (parentTaskTimer != null)
            {
                parentTaskTimer.AddChild(taskTimer);
            }

            return taskTimer;
        }

        public ITaskTimer StartTaskFormat(string formatName, params object[] args)
        {
            var name = SafeStringFormat(formatName, args);
            return StartTask(name);
        }

        public ITaskTimer StartTaskFormat(ITaskTimer parentTaskTimer, string formatName, params object[] args)
        {
            var name = SafeStringFormat(formatName, args);
            return StartTask(name, parentTaskTimer);
        }

        private void DisposeTaskTimer(TaskTimer taskTimer)
        {
            if (taskTimer.IsDisposed)
            {
                // No need to do anything at all!
                return;
            }

            taskTimer.StopedTime = DateTime.Now;
            if (taskTimer.Parent != null)
            {
                return;
            }

            var stringBuilder = new StringBuilder();
            RenderTaskTimer(taskTimer, stringBuilder, 0);
            Information(stringBuilder.ToString());
        }

        private static void RenderTaskTimer(ITaskTimer taskTimer, StringBuilder stringBuilder, int indent)
        {
            var time = taskTimer.StopedTime.HasValue
                ? string.Format("took {0} sec to complete", (taskTimer.StopedTime.Value - taskTimer.StartedTime).TotalSeconds)
                : "did not finish";

            stringBuilder.AppendFormat(
                "{0}Task '{1}' {2}{3}",
                new string(' ', indent * 2),
                taskTimer.Name,
                time,
                Environment.NewLine);
            foreach (var child in taskTimer.Children)
            {
                RenderTaskTimer(child, stringBuilder, indent + 1);
            }
        }

        protected string SafeStringFormat(string format, params object[] args)
        {
            String message;
            try
            {
                message = string.Format(format, args);
            }
            catch (Exception)
            {
                /* Never throw on log! */
                try
                {
                    message = string.Format(
                        "<exception while formatting message: '{0}' with args: '{1}'>",
                        format,
                        string.Join("', '", args.Select(arg => arg.ToString())));
                }
                catch (Exception)
                {
                    message = string.Format("<exception while formatting message: '{0}'>", format);
                }
            }

            return message;
        }


    }
}
