using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class InvalidTimeFormatException : Exception
    {
        public InvalidTimeFormatException() : base("Invalid time format, please use HH:MM.") { }
    }
}
