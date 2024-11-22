using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExample
{
    class Employee
    {
        public string Name { get; set; }  
        public double Salary { get; set; } 
        public string Department { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of employees
            List<Employee> employees = new List<Employee>
            {
                new Employee { Name = "John", Salary = 45000, Department = "HR" },
                new Employee { Name = "Sara", Salary = 60000, Department = "IT" },
                new Employee { Name = "David", Salary = 70000, Department = "IT" },
                new Employee { Name = "Rachel", Salary = 55000, Department = "Finance" },
                new Employee { Name = "Michael", Salary = 40000, Department = "HR" },
                new Employee { Name = "Linda", Salary = 80000, Department = "IT" },
                new Employee { Name = "James", Salary = 30000, Department = "Finance" }
            };

            // LINQ query to find employees earning more than 50,000
            var highEarners = from e in employees  // Start LINQ query on 'employees' list
                              where e.Salary > 50000  // Filter employees whose salary is greater than 50,000
                              select e;  // Select the employee objects that match the condition

            // Display employees earning more than 50,000
            Console.WriteLine("Employees earning more than $50,000:");
            foreach (var employee in highEarners)
            {
                Console.WriteLine($"{employee.Name}: ${employee.Salary}");
            }


            // LINQ query to sort employees by salary in descending order
            var sortedBySalary = from e in employees  // Start LINQ query on 'employees' list
                                 orderby e.Salary descending  // Sort employees by salary in descending order
                                 select e;  // Select the employee objects

            // Display employees sorted by salary in descending order
            Console.WriteLine("\nEmployees sorted by salary (highest to lowest):");
            foreach (var employee in sortedBySalary)
            {
                Console.WriteLine($"{employee.Name}: ${employee.Salary}");
            }


            // LINQ query to group employees by department and count the number of employees in each department
            var groupedByDepartment = from e in employees  
                                      group e by e.Department into deptGroup  // Group employees by department
                                      select new  // Create a new anonymous object for each department group
                                      {
                                          Department = deptGroup.Key,  // The key (department) of the group
                                          Count = deptGroup.Count()  // The number of employees in that department
                                      };

            // Display the count of employees in each department
            Console.WriteLine("\nEmployee count by department:");
            foreach (var group in groupedByDepartment)
            {
                Console.WriteLine($"{group.Department}: {group.Count} employees");
            }
        }
    }

/*
     
Employees earning more than $50,000:
Sara: $60000
David: $70000
Linda: $80000

Employees sorted by salary (highest to lowest):
Linda: $80000
David: $70000
Sara: $60000
Rachel: $55000
John: $45000
Michael: $40000
James: $30000

Employee count by department:
HR: 2 employees
IT: 3 employees
Finance: 2 employees
 
 */
}
