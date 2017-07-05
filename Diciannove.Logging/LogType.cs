using System;

namespace Diciannove.Logging
{
    [Flags]
    public enum LogType
    {
        Debug = 1,
        Info = 2,
        Warn = 4,
        Error = 8,
        Fatal = 16,
        Other = 32
    }
}