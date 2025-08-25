using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class MissingTimeEntryException : Exception
    {
        public MissingTimeEntryException() : base("Missing clock out entry.") { }
    }
}
