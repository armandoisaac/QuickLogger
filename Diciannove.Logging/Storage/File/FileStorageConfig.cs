using System;
using System.IO;

namespace Diciannove.Logging.Storage.File
{
    /// <summary>
    /// A configuration for the FileStorage
    /// </summary>
    internal class FileStorageConfig
    {
        /// <summary>
        /// The path where the file will be saved
        /// </summary>
        public string StoragePath { get; set; }

        /// <summary>
        /// The prefix of the file.
        /// </summary>
        public string FilePrefix { get; set; }

        /// <summary>
        /// The max size (in kilobytes) of the output file
        /// </summary>
        public ulong MaxFileSize { get; set; }

        public string FileName => Path.Combine(StoragePath, FilePrefix);

        internal FileStorageConfig()
        {
            StoragePath = AppContext.BaseDirectory;
            FilePrefix = "log";
            MaxFileSize = 10240;
        }
    }
}