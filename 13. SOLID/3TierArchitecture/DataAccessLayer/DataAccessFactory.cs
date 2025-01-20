using DataAccessLayer.Models;
using DataAccessLayer.Repos;
using System;

namespace DataAccessLayer
{
    // Factory class that provides access to the data repository.
    public class DataAccessFactory
    {
        // It abstracts the creation of the repository, allowing the application to work with repositories generically.
        public static IRepository<Employee, int> EmployeeDataAccess(Context context)
        {
            return new EmpRepos(context);  // Returns an instance of EmpRepos for Employee.
        }
    }
}

/*
 
The DataAccessFactory class abstracts the creation of repositories, 
responsible for creating and providing access to the appropriate repository for performing data operations
In this case, it creates and returns an IRepository specifically for the Employee entity, using int as the identifier.
The factory pattern makes it easier to manage and maintain repositories by allowing the application to access them without worrying about their creation.
This promotes modularity and flexibility, as the repository implementation can be easily changed or extended.

*/
