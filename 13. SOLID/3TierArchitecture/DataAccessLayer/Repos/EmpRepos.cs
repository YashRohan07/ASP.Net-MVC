using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repos
{
    public class EmpRepos : IRepository<Employee, int>
    {
        private readonly Context db;  // Database context used to interact with the database

        public EmpRepos(Context context)
        {
            // Constructor to initialize the database context
            db = context ?? throw new ArgumentNullException(nameof(context));  // Throw an exception if context is null
        }

        // Add a new employee
        public void Add(Employee e)
        {
            db.Employees.Add(e);  // Add the new employee to the Employees DbSet
            db.SaveChanges();  // Save changes to the database
        }

        // Get all employees
        public List<Employee> Get()
        {
            return db.Employees.ToList();  // Retrieve all employee entities from the database
        }

        // Get an employee by id
        public Employee Get(int id)
        {
            return db.Employees.Find(id);  // Find and return an employee by their ID
        }

        // Update an existing employee
        public void Update(Employee e)
        {
            var exobj = Get(e.Id);  // Get the existing employee by ID
            if (exobj != null)  // Check if the employee exists
            {
                db.Entry(exobj).CurrentValues.SetValues(e);  // Update the existing employee with new values
                db.SaveChanges();  
            }
        }

        // Delete an employee by id
        public void Delete(int id)
        {
            var exobj = Get(id);  // Get the employee by ID
            if (exobj != null)  // Check if the employee exists
            {
                db.Employees.Remove(exobj);  // Remove the employee from the Employees DbSet
                db.SaveChanges(); 
            }
        }
    }
}

/*
 
The `EmpRepos` class implements the `IRepository` interface and provides data access operations for the `Employee` entity. 
It interacts with the database context (`Context`) to perform CRUD operations like adding, retrieving, updating, and deleting employee records.
The methods ensure that changes to the database are persisted and allow the application to perform data operations in an abstracted and reusable way. 
By implementing generic repository patterns, this class provides a flexible and consistent approach to managing `Employee` data.

*/

