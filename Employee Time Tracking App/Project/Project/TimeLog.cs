using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class TimeLog
    {
       
        // Represents a time log for an employee's clock-in and clock-out times.
        public int EmployeeID { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }

        
        // Initializes a new instance of the TimeLog class with the specified employee ID and clock-in time.
        public double CalculateHours()
        {
            if (ClockOutTime.HasValue)
            {
                return (ClockOutTime.Value - ClockInTime).TotalHours;
            }
            return 0;
        }

        public bool LateArrival()// Checks if the employee arrived late based on a standard start time.
        {
            // Assuming a standard start time of 9 AM
            return ClockInTime.TimeOfDay > new TimeSpan(9, 0, 0);
        }
    }
}
