using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project
{
    public class FileChecker// Monitors a directory for new files and processes them to log employee clock-in and clock-out times.
    {
        private string Path;// The path to the directory being monitored.
        public event Action<string> OnNewFileDetected;

        private readonly TimeLogManager _manager;

        public FileChecker(string folderPath, TimeLogManager manager)// Constructor to initialize the FileChecker with a folder path and a TimeLogManager instance.
        {
            Path = folderPath;
            _manager = manager;
            Directory.CreateDirectory(Path);
        }

        public void WatchDirectory()// Starts watching the specified directory for new files.
        {
            FileSystemWatcher watcher = new FileSystemWatcher(Path);
            watcher.Created += (s, e) =>
            {
                Console.WriteLine($"New file detected: {e.FullPath}");
                OnNewFileDetected?.Invoke(e.FullPath);

                try
                {
                    ProcessFile(e.FullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Could not process {e.FullPath}: {ex.Message}");
                }
            };
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching folder: {Path}");
            Thread.Sleep(Timeout.Infinite);
        }

        private void ProcessFile(string filePath)// Processes the specified file to log employee clock-in and clock-out times.
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 3) continue;

                int employeeId = int.Parse(parts[0]);
                string action = parts[1].Trim().ToUpper();
                string timeStr = parts[2].Trim();

                if (!_manager.ValidateTimeFormat(timeStr))
                    throw new InvalidTimeFormatException();

                DateTime time = DateTime.Today.Add(TimeSpan.Parse(timeStr));

                if (action == "CLOCKIN") // Handles clock-in actions for employees.
                {
                    _manager.Logs.Add(new TimeLog
                    {
                        EmployeeID = employeeId,
                        ClockInTime = time
                    });
                    Console.WriteLine($"Employee {employeeId} clocked IN at {time}");
                }
                else if (action == "CLOCKOUT")
                {
                    var log = _manager.Logs.LastOrDefault(l => l.EmployeeID == employeeId && !l.ClockOutTime.HasValue);
                    if (log == null) throw new MissingTimeEntryException();

                    log.ClockOutTime = time;
                    Console.WriteLine($"Employee {employeeId} clocked OUT at {time}");
                }
            }
        }
    }
}
