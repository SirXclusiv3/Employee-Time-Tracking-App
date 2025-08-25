using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project
{
    internal class Program
    {
        static Admin admin = new Admin("admin", "admin123");
        static TimeLogManager timeLogManager = new TimeLogManager();
        static ReportGenerator reportGenerator = new ReportGenerator();
        static FileChecker fileChecker = new FileChecker("C:\\Logs", timeLogManager);

        static void Main(string[] args) 
        {
            Console.WriteLine("=== Employee Time Tracking System ===");

            if (!Login()) // Prompt the user for login credentials and validate them
            {
                Console.WriteLine("Login failed. Exiting...");
                return;
            }

            Task.Run(() => fileChecker.WatchDirectory());
            Task.Run(() => new AutoReportThread(reportGenerator, timeLogManager.Logs).Run());

            bool exit = false;
            while (!exit) 
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Clock In");
                Console.WriteLine("3. Clock Out");
                Console.WriteLine("4. Generate Report");
                Console.WriteLine("5. Export Report to CSV");
                Console.WriteLine("6. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        ClockIn();
                        break;
                    case "3":
                        ClockOut();
                        break;
                    case "4":
                        GenerateReport();
                        break;
                    case "5":
                        ExportReport();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            Console.WriteLine("System shutdown. Goodbye!");
        }

        static bool Login() // Prompts the user for login credentials and validates them against the admin's credentials
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            return admin.Login(username, password);
        }

        static void AddEmployee() // Prompts the user to add a new employee, either full-time or part-time
        {
            Console.Write("Employee Name: ");
            string name = Console.ReadLine();
            Console.Write("Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }
            Console.Write("Full-time? (y/n): ");
            string type = Console.ReadLine();

            Employee emp; // Declare an Employee variable to hold the new employee instance
            if (type.ToLower() == "y")
            {
                emp = new FullTimeEmployee(name, id);
            }
            else
            {
                emp = new PartTimeEmployee(name, id);
            }

            admin.AddEmployee(emp);
            Console.WriteLine("Employee added successfully.");
        }

        static void ClockIn()// Prompts the user to clock in an employee, validating the input and recording the time
        {
            Console.Write("Employee ID: ");
            string idStr = Console.ReadLine();
            Console.Write("Clock-in Time (HH:mm): ");
            string timeStr = Console.ReadLine();

            try
            {
                if (!int.TryParse(idStr, out int id))
                {
                    Console.WriteLine("Invalid ID format.");
                    return;
                }
                var employee = admin.GetEmployeeById(idStr);
                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                    return;
                }

                if (!timeLogManager.ValidateTimeFormat(timeStr))
                {
                    throw new InvalidTimeFormatException();
                }

                employee.ClockIn(timeLogManager);
                Console.WriteLine("Clock-in recorded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ClockOut() // Prompts the user to clock out an employee, validating the input and recording the time
        {
            Console.Write("Employee ID: ");
            string idStr = Console.ReadLine();
            Console.Write("Clock-out Time (HH:mm): ");
            string timeStr = Console.ReadLine();

            try
            {
                if (!int.TryParse(idStr, out int id))
                {
                    Console.WriteLine("Invalid ID format.");
                    return;
                }
                var employee = admin.GetEmployeeById(idStr);
                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                    return;
                }

                if (!timeLogManager.ValidateTimeFormat(timeStr))
                {
                    throw new InvalidTimeFormatException();
                }

                employee.ClockOut(timeLogManager);
                Console.WriteLine("Clock-out recorded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GenerateReport() // Prompts the user to select a report type and generates the corresponding report
        {
            Console.WriteLine("Report Type:");
            Console.WriteLine("1. Weekly");
            Console.WriteLine("2. Monthly");
            Console.WriteLine("3. Overtime");
            Console.WriteLine("4. Late Arrivals");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            List<TimeLog> allLogs = timeLogManager.Logs;

            switch (choice)
            {
                case "1":
                    reportGenerator.GenerateWeeklyReport(allLogs);
                    break;
                case "2":
                    reportGenerator.GenerateMonthlyReport(allLogs);
                    break;
                case "3":
                    reportGenerator.GenerateOvertimeReport(allLogs);
                    break;
                case "4":
                    reportGenerator.GenerateLateArrivalReport(allLogs);
                    break;
                default:
                    Console.WriteLine("Invalid report type.");
                    return;
            }
            Console.WriteLine("Report generated. Check the output file.");
        }

        static void ExportReport()// Prompts the user for a filename and exports the report to a CSV file
        {
            Console.Write("Filename for CSV export: ");
            string filename = Console.ReadLine();

            List<TimeLog> allLogs = timeLogManager.Logs;
            reportGenerator.GenerateWeeklyReport(allLogs);

            var reports = reportGenerator.GetReportsByDateRange(DateTime.MinValue, DateTime.MaxValue);
            reportGenerator.ExportReportToCsv(filename, reports);
            Console.WriteLine("Report exported successfully.");
        }
    }
}