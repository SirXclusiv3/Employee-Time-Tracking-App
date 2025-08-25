using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    // Event argument for missing time entries
    public class MissingTimeEntryEventArgs : EventArgs
    {
        public int EmployeeID { get; }
        public DateTime Date { get; }

        public MissingTimeEntryEventArgs(int employeeId, DateTime date)
        {
            EmployeeID = employeeId;
            Date = date;
        }
    }

    public interface IReportGenerator// Interface for generating various types of reports
    {
        void GenerateWeeklyReport(List<TimeLog> logs);
        void GenerateMonthlyReport(List<TimeLog> logs);
        void GenerateOvertimeReport(List<TimeLog> logs);
        void GenerateLateArrivalReport(List<TimeLog> logs);
        void ExportReportToCsv(string filePath, List<EmployeeReport> reports);
        void ExportReportToPdf(string filePath);
        event EventHandler<MissingTimeEntryEventArgs> OnMissingTimeEntry;
        List<EmployeeReport> GetReportsByDateRange(DateTime start, DateTime end);
        List<EmployeeReport> GetReportsByDepartment(string departmentId);
    }


    // Optional class to represent employee report data
    public class EmployeeReport
    {
        public int EmployeeID { get; set; }
        public double TotalHours { get; set; }
        public int LateArrivals { get; set; }
        public double OvertimeHours { get; set; }
        public string EmployeeName { get; internal set; }
        public double TotalPay { get; internal set; }
    }
}
