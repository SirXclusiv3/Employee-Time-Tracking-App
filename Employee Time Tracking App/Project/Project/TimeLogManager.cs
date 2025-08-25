using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class TimeLogManager // Manages time logs for employees
    {
        public List<TimeLog> Logs { get; } = new List<TimeLog>(); // Collection of time logs

        public bool ValidateTimeFormat(string timeStr)
        {
            return TimeSpan.TryParse(timeStr, out _);
        }
    }
}
