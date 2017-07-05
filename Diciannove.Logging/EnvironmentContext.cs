using System;

namespace Diciannove.Logging
{
    public class EnvironmentContext
    {
        public string MachineName { get; set; }

        internal EnvironmentContext()
        {
            MachineName = Environment.GetEnvironmentVariable("CUMPUTERNAME") ?? Environment.GetEnvironmentVariable("HOSTNAME");
        }
    }
}