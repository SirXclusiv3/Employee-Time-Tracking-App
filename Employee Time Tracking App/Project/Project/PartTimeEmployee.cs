using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Project
{
    public class PartTimeEmployee : Employee// Represents a part-time employee in the system
    {
        private const double HourlyRate = 25;

        public PartTimeEmployee(string name, int id) : base(name, "defaultUsername")
        {
            this.Name = name;
            this.Id = id;
        }

        public override double CalculatePay(double hoursWorked)// Calculates the pay for a part-time employee based on hours worked and hourly rate.
        {
            return hoursWorked * HourlyRate;
        }
    }
}
