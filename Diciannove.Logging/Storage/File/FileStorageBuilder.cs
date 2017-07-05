using System;
using System.IO;

namespace Diciannove.Logging.Storage.File
{
    public class FileStorageBuilder
    {
        private readonly FileStorageConfig _config;

        public FileStorageBuilder()
        {
            _config = new FileStorageConfig();
        }

        public FileStorageBuilder WithStoragePath(string path)
        {
            // Validate the path
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path), "The path cannot be empty");

            // Try to create the path
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _config.StoragePath = path;

            return this;
        }

        public FileStorageBuilder WithFilePrefix(string prefix)
        {
            // Validate the prefix
            if (string.IsNullOrEmpty(prefix)) throw new ArgumentNullException(nameof(prefix), "The file prefix cannot be empty");

            _config.FilePrefix = prefix;
            return this;
        }

        public FileStorageBuilder WithMaxFileSize(ulong maxFileSize)
        {
            // Validate the file size
            if (maxFileSize == 0) throw new ArgumentOutOfRangeException(nameof(maxFileSize), "The max file size cannot be zero.");
            _config.MaxFileSize = maxFileSize * 1000;
            return this;
        }
        
        /// <summary>
        /// Builds a new instance of the FileStorage
        /// </summary>
        /// <returns></returns>
        public FileStorage Build()
        {
            return new FileStorage(_config);
        }
    }
}