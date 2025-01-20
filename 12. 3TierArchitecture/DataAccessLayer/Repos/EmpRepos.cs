using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class EmpRepos
    {
        private readonly Context _context;

        // Constructor to inject the context (Database context)
        public EmpRepos(Context context)
        {
            _context = context;
        }


        // Add a new employee
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }


        // Get employee by Id
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }


        // Get all employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList(); // Convert to List 
        }


        // Update employee information
        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
            if (existingEmployee != null)
            {
                // Update the existing employee's properties
                existingEmployee.Name = employee.Name;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.JoinDate = employee.JoinDate;
                _context.SaveChanges(); // Save the changes to the database
            }
        }


        // Delete an employee by Id
        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee); // Removes the employee entity
                _context.SaveChanges();
                return true;
            }
            return false; // Returns false if the employee wasn't found
        }


        // Search for employees based on optional filters
        public IEnumerable<Employee> SearchEmployees(string name, string salary, int? id, string joinDate)
        {
            var query = _context.Employees.AsQueryable(); // Start with the Employees table as the query

            // Filter by Id 
            if (id.HasValue)
            {
                query = query.Where(e => e.Id == id.Value);
            }

            // Filter by name
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            // Filter by salary
            if (!string.IsNullOrEmpty(salary))
            {
                query = query.Where(e => e.Salary == salary);
            }

            // Filter by join date if provided
            if (!string.IsNullOrEmpty(joinDate))
            {
                if (DateTime.TryParse(joinDate, out DateTime parsedDate))
                {
                    query = query.Where(e => e.JoinDate.Date == parsedDate.Date); // Matching the exact date
                }
            }

            return query.ToList(); // Materialize the result to List
        }
    }
}

