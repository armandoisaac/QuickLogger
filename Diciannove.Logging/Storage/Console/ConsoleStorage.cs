using System;

namespace Diciannove.Logging.Storage.Console
{
    /// <summary>
    ///     A class that outputs the log message to the console
    /// </summary>
    public class ConsoleStorage : ILogStorage
    {
        /// <summary>
        ///     Outputs the log message to the console. No save is performed.
        /// </summary>
        /// <param name="message"></param>
        public void Save(LogMessage message)
        {
            // Save the original foreground color
            var foregroundColor = System.Console.ForegroundColor;

            // Set the foreground color according the log type
            System.Console.ForegroundColor = GetForegroundColor(message.Type);

            // Output the message
            System.Console.WriteLine(message.ToString());

            // Restore the foreground color
            System.Console.ForegroundColor = foregroundColor;
        }

        /// <summary>
        ///     Gets the foreground color for the console according the log type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ConsoleColor GetForegroundColor(LogType type)
        {
            switch (type)
            {
                case LogType.Debug: return ConsoleColor.Gray;
                case LogType.Info: return ConsoleColor.Green;
                case LogType.Warn: return ConsoleColor.Cyan;
                case LogType.Error: return ConsoleColor.Magenta;
                case LogType.Fatal: return ConsoleColor.Red;
            }
            return ConsoleColor.Black;
        }
    }
}