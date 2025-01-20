using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repos;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService
    {
        // Dependency variables
        private readonly IRepository<Employee, int> empRepos;  // Repository for CRUD operations on Employee entities
        private readonly IMapper mapper;  // AutoMapper instance for mapping between Employee and EmployeeDTO


        // Constructor to initialize the dependencies (repository and AutoMapper)
        public EmployeeService()
        {
            // Initialize the database context and repository for Employee data access
            var context = new Context();
            empRepos = DataAccessFactory.EmployeeDataAccess(context);  // Factory method to get the repository

            // Configure AutoMapper to map between Employee and EmployeeDTO objects
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap());
            mapper = config.CreateMapper();  // Create the AutoMapper instance
        }


        public void AddEmployee(EmployeeDTO employeeDto)
        {
            // Map the EmployeeDTO to Employee entity
            var employee = mapper.Map<Employee>(employeeDto);

            // Add the employee to the repository
            empRepos.Add(employee);
        }


        // Get an employee from the repository by their unique ID
        public EmployeeDTO GetEmployeeById(int id)
        {
            var employee = empRepos.Get(id);

            // Map the Employee entity to EmployeeDTO and return it
            return mapper.Map<EmployeeDTO>(employee);
        }


        // Method to get all employees from the repository
        public List<EmployeeDTO> GetAllEmployees()
        {
            // Retrieve all Employee entities from the repository
            var employees = empRepos.Get();

            // Map the list of Employee entities to a list of EmployeeDTOs and return it
            return mapper.Map<List<EmployeeDTO>>(employees);
        }


        // Method to update an existing employee's information in the repository
        public void UpdateEmployee(EmployeeDTO employeeDto)
        {
            // Map the EmployeeDTO to Employee entity
            var employee = mapper.Map<Employee>(employeeDto);
            empRepos.Update(employee);
        }


        // Method to delete an employee from the repository by their unique ID
        public void DeleteEmployee(int id)
        {
            empRepos.Delete(id);
        }


        // Method to search employees based on various filter criteria (name, salary, ID, join date)
        public List<EmployeeDTO> SearchEmployees(string name, string salary, int? id, string joinDate)
        {
            var employees = empRepos.Get().Where(e =>
                (string.IsNullOrEmpty(name) || e.Name.Contains(name)) &&  // Filter by name 
                (string.IsNullOrEmpty(salary) || e.Salary.ToString() == salary) &&  // Filter by salary
                (id == null || e.Id == id) &&  // Filter by employee ID 
                (string.IsNullOrEmpty(joinDate) || e.JoinDate.ToString("yyyy-MM-dd") == joinDate)  // Filter by join date 
            ).ToList();

            // Map the filtered list of Employee entities to a list of EmployeeDTOs and return it
            return mapper.Map<List<EmployeeDTO>>(employees);
        }
    }
}

/*
 
 * The `EmployeeService` class acts as an intermediary between the business logic and the data access layers. 
 * It provides simplified methods for performing CRUD (Create, Read, Update, Delete) operations on Employee entities
 
 * The class uses AutoMapper to map between the `Employee` entity and the `EmployeeDTO`, which ensures that only the 
 * relevant data is passed to the client, avoiding unnecessary exposure of entity-specific fields.
 
 * The `EmployeeService` performs operations by interacting with the `IRepository` interface to ensure flexibility 
 * and consistency in how data is retrieved, updated, and persisted to the database.
 
 */
