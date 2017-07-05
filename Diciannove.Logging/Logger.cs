using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diciannove.Logging
{
    public class Logger : ILogger
    {
        private readonly LoggerConfiguration _configuration;

        internal Logger(LoggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Debug(string message)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Debug
            });
        }

        public void Debug(string message, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Context = context,
                Type = LogType.Debug
            });
        }

        public void Info(string message)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Info
            });
        }

        public void Info(string message, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Context = context,
                Type = LogType.Info
            });
        }

        public void Warn(string message)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Warn
            });
        }

        public void Warn(string message, Exception exception)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Warn
            });
        }

        public void Warn(string message, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Warn,
                Context = context
            });
        }

        public void Warn(string message, Exception exception, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Warn,
                Context = context
            });
        }

        public void Error(string message)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Error
            });
        }

        public void Error(string message, Exception exception)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Error
            });
        }

        public void Error(string message, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Error,
                Context = context
            });
        }

        public void Error(string message, Exception exception, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Error,
                Context = context
            });
        }

        public void Fatal(string message)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Fatal
            });
        }

        public void Fatal(string message, Exception exception)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Fatal
            });
        }

        public void Fatal(string message, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Type = LogType.Fatal,
                Context = context
            });
        }

        public void Fatal(string message, Exception exception, Dictionary<string, object> context)
        {
            Store(new LogMessage
            {
                Message = message,
                Exception = exception,
                Type = LogType.Fatal,
                Context = context
            });
        }

        public void Log(LogMessage logMessage)
        {
            Store(logMessage);
        }

        private void Store(LogMessage message)
        {
            if ((message.Type.HasFlag(LogType.Debug) && _configuration.Types.HasFlag(LogType.Debug)) ||
                (message.Type.HasFlag(LogType.Info) && _configuration.Types.HasFlag(LogType.Info)) ||
                (message.Type.HasFlag(LogType.Warn) && _configuration.Types.HasFlag(LogType.Warn)) ||
                (message.Type.HasFlag(LogType.Error) && _configuration.Types.HasFlag(LogType.Error)) ||
                (message.Type.HasFlag(LogType.Fatal) && _configuration.Types.HasFlag(LogType.Fatal)))
            {
                Task.Factory.StartNew(() =>
                {
                    Parallel.ForEach(_configuration.Output, storage =>
                    {
                        try
                        {
                            storage.Save(message);
                        }
                        catch
                        {
                            if (_configuration.FireAndForget) return;
                            throw;
                        }
                    });
                });
            }
        }
    }
}