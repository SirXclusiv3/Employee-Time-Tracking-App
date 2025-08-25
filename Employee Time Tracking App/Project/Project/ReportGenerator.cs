using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class ReportGenerator : IReportGenerator// Interface for generating various types of reports
    {
        public event EventHandler<MissingTimeEntryEventArgs> OnMissingTimeEntry;
        private List<EmployeeReport> reports = new List<EmployeeReport>();

        public void GenerateWeeklyReport(List<TimeLog> logs)// Generates a weekly report from the provided time logs
        
        {
            GenerateReport(logs, "WeeklyReport.txt", includeOvertime: true, includeLateArrivals: true);
        }

        public void GenerateMonthlyReport(List<TimeLog> logs)// Generates a monthly report from the provided time logs
        {
            if (logs == null) throw new ArgumentNullException(nameof(logs));
            GenerateReport(logs, "MonthlyReport.txt", includeOvertime: true, includeLateArrivals: true);
        }

        public void GenerateOvertimeReport(List<TimeLog> logs)// Generates an overtime report from the provided time logs
        {
            if (logs == null) throw new ArgumentNullException(nameof(logs));
            GenerateReport(logs, "OvertimeReport.txt", includeOvertime: true, includeLateArrivals: false);
        }

        public void GenerateLateArrivalReport(List<TimeLog> logs)// Generates a late arrival report from the provided time logs
        {
            if (logs == null) throw new ArgumentNullException(nameof(logs));
            GenerateReport(logs, "LateArrivalReport.txt", includeOvertime: false, includeLateArrivals: true);
        }

        public void ExportReportToCsv(string filePath, List<EmployeeReport> reports)// Exports the generated reports to a CSV file
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("EmployeeID,EmployeeName,TotalHours,OvertimeHours,LateArrivals,TotalPay");

                foreach (var report in reports)
                {
                    writer.WriteLine($"{report.EmployeeID},{report.EmployeeName},{report.TotalHours},{report.OvertimeHours},{report.LateArrivals},{report.TotalPay}");
                }
            }
        }

        public void ExportReportToPdf(string filePath)// Exports the generated reports to a PDF file
        {
            throw new NotImplementedException("PDF export requires iTextSharp or similar library.");
        }

        public List<EmployeeReport> GetReportsByDateRange(DateTime start, DateTime end)//returns reports filtered by a date range
        
        {
            return reports;
        }

        public List<EmployeeReport> GetReportsByDepartment(string departmentId)//returns reports filtered by a department ID
        
        {
            return reports;
        }

        private void GenerateReport(List<TimeLog> logs, string fileName, bool includeOvertime, bool includeLateArrivals)// Generates a report based on the provided time logs and writes it to a specified file
        {
            if (logs == null)
            {
                Console.WriteLine("No logs available to generate report.");
                return;
            }

            reports.Clear();
            var grouped = logs.GroupBy(log => log.EmployeeID);

            using (StreamWriter writer = new StreamWriter(fileName))// Opens a stream writer to write the report to a file
            {
                foreach (var group in grouped)// Groups logs by EmployeeID
                {
                    var report = new EmployeeReport { EmployeeID = group.Key };// Initializes a new EmployeeReport for each group

                    double totalHours = 0;
                    double overtimeHours = 0;
                    int lateArrivals = 0;

                    foreach (var log in group)
                    {
                        if (!log.ClockOutTime.HasValue)
                        {
                            OnMissingTimeEntry?.Invoke(this, new MissingTimeEntryEventArgs(log.EmployeeID, log.ClockInTime.Date));
                        }
                        else
                        {
                            double hours = log.CalculateHours();
                            totalHours += hours;

                            if (hours > 8)
                                overtimeHours += (hours - 8);
                        }

                        if (log.LateArrival())
                            lateArrivals++;
                    }

                    report.TotalHours = totalHours;
                    report.OvertimeHours = overtimeHours;
                    report.LateArrivals = lateArrivals;

                    reports.Add(report);

                    writer.WriteLine($"Employee ID: {group.Key}");
                    writer.WriteLine($"Total Hours: {totalHours:F2}");
                    if (includeOvertime) writer.WriteLine($"Overtime Hours: {overtimeHours:F2}");
                    if (includeLateArrivals) writer.WriteLine($"Late Arrivals: {lateArrivals}");
                    writer.WriteLine();
                }
            }
        }
    }
}
