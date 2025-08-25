using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface ITimeValidator
    {
        bool ValidateTimeFormat(string time);
        void CheckMissingClockOut(List<TimeLog> logs);
    }
}
