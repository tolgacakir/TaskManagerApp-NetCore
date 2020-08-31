using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaskManagerApp.Core.CrossCuttingConcerns.Logging.Loggers;

namespace TaskManagerApp.Core.CrossCuttingConcerns.Logging.LoggerProviders
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new FileNotFoundException("File path is null or empty.");
                }
                else
                {
                    _filePath = value;
                }
            }
        }

        public FileLoggerProvider(string filePath)
        {
            FilePath = filePath;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose()
        {
        }
    }
}
