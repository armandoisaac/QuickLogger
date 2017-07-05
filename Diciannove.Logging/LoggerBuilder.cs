using System;
using Diciannove.Logging.Storage;

namespace Diciannove.Logging
{
    public class LoggerBuilder
    {
        private readonly LoggerConfiguration _config;

        public LoggerBuilder()
        {
            _config = new LoggerConfiguration();
        }

        /// <summary>
        ///     Enables all log types for storage
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder WithAllLogTypes()
        {
            _config.Types = LogType.Debug | LogType.Info | LogType.Warn | LogType.Error | LogType.Fatal;
            return this;
        }

        /// <summary>
        ///     Defines which log types will be stored
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public LoggerBuilder WithLogTypes(LogType types)
        {
            _config.Types = types;
            return this;
        }

        /// <summary>
        ///     Defines where the log messages will be stored
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public LoggerBuilder WithStorage(params ILogStorage[] output)
        {
            _config.Output.AddRange(output);
            return this;
        }

        /// <summary>
        ///     Swallows all exceptions when storing the log messages
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder WithFireAndForget()
        {
            _config.FireAndForget = true;
            return this;
        }

        /// <summary>
        ///     Allows all exceptions on the storage providers to be sent to the client.
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder WithExceptions()
        {
            _config.FireAndForget = false;
            return this;
        }

        /// <summary>
        /// Validates the configuration and returns an implementation of ILogger
        /// </summary>
        /// <returns></returns>
        public ILogger Build()
        {
            if (_config.Output.Count == 0)
                throw new ArgumentException("Storage cannot be empty. At least one output must be configured");
            return new Logger(_config);
        }
    }
}