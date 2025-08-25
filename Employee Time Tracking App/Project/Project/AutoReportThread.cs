using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Project
{
    public class AutoReportThread
    {
        private readonly IReportGenerator _generator; // Interface for generating reports
        private readonly List<TimeLog> _logs;// List of time logs to generate reports from
        private bool running = true;

        public AutoReportThread(IReportGenerator generator, List<TimeLog> logs)// Constructor to initialize the AutoReportThread with a report generator and logs
        {
            _generator = generator;
            _logs = logs;
        }

        private List<EmployeeReport> BuildReportsFromLogs(List<TimeLog> logs)// Method to build employee reports from the provided time logs
        {
            var reports = new List<EmployeeReport>();
            if (logs == null || logs.Count == 0)
            {
                return reports;
            }

            var groupedLogs = logs.GroupBy(l => l.EmployeeID);
            foreach (var group in groupedLogs)// Group logs by EmployeeID
            {
                double totalHours = group.Sum(l => l.CalculateHours());
                reports.Add(new EmployeeReport
                {
                    EmployeeID = group.Key,
                    TotalHours = totalHours
                });
            }
            return reports;
        }

        public void Run()// Method to start the auto report generation thread
        {
            while (running)
            {
                Thread.Sleep(TimeSpan.FromDays(7)); // Wait for 7 days
                _generator.GenerateWeeklyReport(_logs); // Generate the weekly report
                Console.WriteLine("Auto weekly report generated!");
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}
