using System;
using System.Collections.Generic;

namespace Diciannove.Logging
{
    public interface ILogger
    {
        /// <summary>
        ///     Messages that are only logged for debugging purposes
        /// </summary>
        void Debug(string message);

        /// <summary>
        ///     Messages that are only logged for debugging purposes
        /// </summary>
        void Debug(string message, Dictionary<string, object> context);

        /// <summary>
        ///     Messages that are logged for information that may be useful
        /// </summary>
        void Info(string message);

        /// <summary>
        ///     Messages that are logged for information that may be useful
        /// </summary>
        /// <param name="message">The information message</param>
        /// <param name="context">A key-pair value list of context values that are helpful to the log data</param>
        void Info(string message, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate a potential problem, though it does not immediately impact the system
        /// </summary>
        void Warn(string message);

        /// <summary>
        ///     Indicate a potential problem, though it does not immediately impact the system
        /// </summary>
        void Warn(string message, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate a potential problem, though it does not immediately impact the system
        /// </summary>
        void Warn(string message, Exception exception);

        /// <summary>
        ///     Indicate a potential problem, though it does not immediately impact the system
        /// </summary>
        void Warn(string message, Exception exception, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate an exception occurred, disrupting some thread or aspect of the system, but leaving the overall system
        ///     intact.
        /// </summary>
        void Error(string message);

        /// <summary>
        ///     Indicate an exception occurred, disrupting some thread or aspect of the system, but leaving the overall system
        ///     intact.
        /// </summary>
        void Error(string message, Exception exception);

        /// <summary>
        ///     Indicate an exception occurred, disrupting some thread or aspect of the system, but leaving the overall system
        ///     intact.
        /// </summary>
        void Error(string message, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate an exception occurred, disrupting some thread or aspect of the system, but leaving the overall system
        ///     intact.
        /// </summary>
        void Error(string message, Exception exception, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate an error has caused an entire sub process or application to stop.
        /// </summary>
        void Fatal(string message);

        /// <summary>
        ///     Indicate an error has caused an entire sub process or application to stop.
        /// </summary>
        void Fatal(string message, Exception exception);

        /// <summary>
        ///     Indicate an error has caused an entire sub process or application to stop.
        /// </summary>
        void Fatal(string message, Dictionary<string, object> context);

        /// <summary>
        ///     Indicate an error has caused an entire sub process or application to stop.
        /// </summary>
        void Fatal(string message, Exception exception, Dictionary<string, object> context);

        void Log(LogMessage logMessage);
    }
}