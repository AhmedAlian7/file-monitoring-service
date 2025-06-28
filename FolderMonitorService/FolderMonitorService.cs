using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace FolderMonitorService
{
    public partial class FolderMonitorService : ServiceBase
    {
        private string _logFilePath;
        private string _logDirectory;
        private const string _fileName = "FolderMonitorServiceLogs.txt";

        private string _sourceFolder;
        private string _destinationFolder;

        private FileSystemWatcher _fileSystemWatcher;

        public FolderMonitorService()
        {
            InitializeComponent();

            _logDirectory = ConfigurationManager.AppSettings["LogDirectory"];
            if (string.IsNullOrEmpty(_logDirectory))
            {
                throw new ConfigurationErrorsException("LogDirectory is not configured in App.config");
            }
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
            _logFilePath = Path.Combine(_logDirectory, _fileName);

            _sourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
            if (string.IsNullOrEmpty(_sourceFolder))
            {
                throw new ConfigurationErrorsException("SourceFolder is not configured in App.config");
            }
            if (!Directory.Exists(_sourceFolder))
            {
                Directory.CreateDirectory(_sourceFolder);
            }

            _destinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
            if (string.IsNullOrEmpty(_destinationFolder))
            {
                throw new ConfigurationErrorsException("DestinationFolder is not configured in App.config");
            }
            if (!Directory.Exists(_destinationFolder))
            {
                Directory.CreateDirectory(_destinationFolder);
            }
        }

        protected override void OnStart(string[] args)
        {
            LogToFile("Service started");
            InitializeWatcher();
        }

        protected override void OnStop()
        {
            LogToFile("Service stopped");

            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.EnableRaisingEvents = false;
                _fileSystemWatcher.Dispose();
                _fileSystemWatcher = null;
            }
        }

        private void InitializeWatcher()
        {

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = _sourceFolder,
                NotifyFilter = NotifyFilters.FileName
                | NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName,
                Filter = "*.*"
            };
            _fileSystemWatcher.Created += OnChanged;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.ChangeType != WatcherChangeTypes.Created)
                {
                    return; // Only process created files
                }

                // Optional: Wait until file is fully written (to avoid file lock)
                Thread.Sleep(1000);

                LogToFile($"File created: {e.FullPath}");

                string newFileName = GenerateUniqueFileName(e.FullPath); // GUID + file extension
                string destinationPath = Path.Combine(_destinationFolder, newFileName);

                File.Move(e.FullPath, destinationPath);

                LogToFile($"File moved: {e.FullPath} to {destinationPath}");
            }
            catch (Exception ex)
            {
                LogToFile($"Error processing file {e.Name}: {ex.Message}");
            }
        }

        private string GenerateUniqueFileName(string originalPath)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(originalPath);
        }

        private void LogToFile(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";
            File.AppendAllText(_logFilePath, logMessage);

            if(Environment.UserInteractive)
            {
                Console.WriteLine(logMessage); // Output to console in debug mode
            }
        }

        [Conditional("DEBUG")]
        public void StartOnConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();
            OnStop();

        }
    }
}
