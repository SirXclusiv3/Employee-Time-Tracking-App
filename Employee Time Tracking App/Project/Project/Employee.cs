using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public abstract class Employee : User // Represents an employee in the system, inheriting from User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }

        public Employee(string username, string password) : base(username, password)
        { }

        public virtual void ClockIn(TimeLogManager manager) // Records the clock-in time for the employee
        {
            manager.Logs.Add(new TimeLog
            {
                EmployeeID = Id,
                ClockInTime = DateTime.Now
            });
        }

        public virtual void ClockOut(TimeLogManager manager) // Records the clock-out time for the employee
        {
            var log = manager.Logs.LastOrDefault(l => l.EmployeeID == Id && !l.ClockOutTime.HasValue);
            if (log == null)
                throw new MissingTimeEntryException();

            log.ClockOutTime = DateTime.Now;
        }

        public abstract double CalculatePay(double hoursWorked);
    }
}
