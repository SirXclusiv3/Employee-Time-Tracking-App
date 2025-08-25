using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Admin : User
    {
        
        
            public List<Employee> Employees { get; set; } = new List<Employee>();// Collection of employees managed by the admin

        public Admin(string username, string password) : base(username, password)
            {
            }

            public void AddEmployee(Employee employee)// Adds a new employee to the admin's list of employees
        {
                Employees.Add(employee);
            }

            public bool Login(string username, string password)
            {
                return this.UserName == username && this.Password == password;
            }

            public Employee GetEmployeeById(string id)// Retrieves an employee by their ID
        {
                if (!int.TryParse(id, out int employeeId))
                {
                    return null;
                }
                return Employees.FirstOrDefault(e => e.Id == employeeId);
            }

            public List<Employee> GetEmployees()// Retrieves the list of all employees managed by the admin
        {
                return Employees;
            }

            public Employee GetEmployeeByUsername(string username)// Retrieves an employee by their username
        {
                return Employees.FirstOrDefault(e => e.UserName == username);
            }
    }
}
