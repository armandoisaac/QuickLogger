using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diciannove.Logging.Storage.File 
{
    public class FileStorage : ILogStorage
    {
        private readonly FileStorageConfig _config;
        private readonly object _lock = new object();
        private DateTime _currentDate;

        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        internal FileStorage(FileStorageConfig config)
        {
            _config = config;
            _currentDate = DateTime.Now.Date;

            // Open the file and make it ready for writing
            OpenFile();
        }

        public void Save(LogMessage message)
        {
            // Fire the save in async mode. This will not block the UI
            Task.Factory.StartNew(() =>
            {
                // We can log one message at the time only
                lock (_lock)
                {
                    // Rotate the file if needed
                    RotateOutputFile();

                    // Save to file
                    _streamWriter.WriteLine(message.ToString());
                    _streamWriter.Flush();
                }
            });
        }

        private void OpenFile()
        {
            var fileName = string.Format("{0}_{1:yyyy-MM-dd}.log", _config.FilePrefix, _currentDate);
            var fullPath = Path.Combine(_config.StoragePath, fileName);

            // Open the file
            _fileStream = System.IO.File.Open(fullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

            // Create the stream to start writing
            _streamWriter = new StreamWriter(_fileStream, Encoding.UTF8);
        }

        private void RotateOutputFile()
        {
            // If the date has changed, rotate the file
            if (_currentDate.Date > DateTime.Now.Date)
            {
                // Close the current file
                _streamWriter.Dispose();
                _fileStream.Dispose();

                // Update the current date
                _currentDate = DateTime.Now.Date;

                // Re-open the file
                OpenFile();
            }
            else
            // If the file size if greater, rotate
            if (_fileStream.Length > (long)_config.MaxFileSize)
            {
                var currentFileName = string.Format("{0}_{1:yyyy-MM-dd}.log", _config.FilePrefix, _currentDate);
                var currentFile = Path.Combine(_config.StoragePath, currentFileName);

                // Close the current file
                _streamWriter.Dispose();
                _fileStream.Dispose();

                // Get the total amount of files with the same name
                var searchPattern = string.Format("{0}_{1:yyyy-MM-dd}*.log", _config.FilePrefix, _currentDate);
                var count = Directory.EnumerateFiles(_config.StoragePath, searchPattern).Count();

                // Move the file
                var newFileName = string.Format("{0}_{1:yyyy-MM-dd}_{2}.log", _config.FilePrefix, _currentDate, count);
                var newFile = Path.Combine(_config.StoragePath, newFileName);
                System.IO.File.Move(currentFile, newFile);

                // Re-open the file again
                OpenFile();
            }
        }
    }
}