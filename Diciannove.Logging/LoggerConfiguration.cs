using System.Collections.Generic;
using Diciannove.Logging.Storage;

namespace Diciannove.Logging
{
    public class LoggerConfiguration
    {
        public readonly List<ILogStorage> Output;
        public bool FireAndForget;
        public LogType Types;

        public LoggerConfiguration()
        {
            Output = new List<ILogStorage>();
            Types = LogType.Warn | LogType.Error | LogType.Fatal;
            FireAndForget = true;
        }
    }
}