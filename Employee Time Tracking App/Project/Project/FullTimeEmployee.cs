using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Project
{
    public class FullTimeEmployee : Employee// Represents a full-time employee in the system
    {
        private const double BasePay = 300;
        private const double OvertimeRate = 50;

        public FullTimeEmployee(string name, int id) : base(name, "defaultUsername")
        {
            this.Name = name;
            this.Id = id;
        }

        public override double CalculatePay(double hoursWorked)// Calculates the pay for a full-time employee based on hours worked, including overtime if applicable.
        {
            double overtimeHours = Math.Max(0, hoursWorked - 8);
            return BasePay + (overtimeHours * OvertimeRate);
        }
    }
}