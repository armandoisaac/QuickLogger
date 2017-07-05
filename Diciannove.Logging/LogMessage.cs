using System;
using System.Collections.Generic;
using System.Text;

namespace Diciannove.Logging
{
    public class LogMessage
    {
        public LogMessage()
        {
            LogDate = DateTime.UtcNow;
        }

        public LogType Type { get; set; }

        /// <summary>
        ///     The timestamp where the message was created. UTC time.
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        ///     The log message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     If applies, the exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     A key-value list of elements that can help to debug issues
        /// </summary>
        public Dictionary<string, object> Context { get; set; }

        /// <summary>
        ///     Some environment properties
        /// </summary>
        public EnvironmentContext EnvironmentContext { get; set; }

        /// <summary>
        /// Overrides the ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Create the message
            var builder = new StringBuilder();

            // Output the message
            builder.AppendLine(string.Format("{0} - {1}", LogDate, Message));

            // Exception
            if (Exception != null)
            {
                builder.AppendLine(Exception.Message);
                if (!string.IsNullOrEmpty(Exception.StackTrace))
                    builder.AppendLine(Exception.StackTrace);

                if (Exception.InnerException != null)
                {
                    builder.AppendLine("Inner Exception:");
                    builder.AppendLine("\t" + Exception.InnerException.Message);
                    if (!string.IsNullOrEmpty(Exception.InnerException.StackTrace))
                        builder.AppendLine("\t" + Exception.InnerException.StackTrace);
                }
            }

            // Output the context
            if (Context != null && Context.Keys.Count > 0)
            {
                builder.AppendLine("Context:");
                foreach (var item in Context)
                    builder.Append(" " + item.Key + ": " + item.Value);
            }

            return builder.ToString();
        }
    }
}