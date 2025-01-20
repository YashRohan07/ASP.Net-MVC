using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace _3TierArchitecture.Controllers
{
    public class EmployeeController : ApiController
    {
        // Service instance that handles business logic for Employee operations
        private readonly EmployeeService empService = new EmployeeService();

        [HttpGet]
        [Route("api/employee/getall")]
        public IHttpActionResult GetAllEmployees()
        {
            // Retrieve all employees from the service
            var employees = empService.GetAllEmployees();

            if (employees.Any())
                return Ok(new { Message = "Employees retrieved successfully.", Data = employees });

            return NotFound();
        }

       
        [HttpGet]
        [Route("api/employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            // Retrieve an employee by ID from the service
            var employee = empService.GetEmployeeById(id);

            if (employee != null)
                return Ok(new { Message = "Employee retrieved successfully.", Data = employee });

            return NotFound();
        }

      
        [HttpPost]
        [Route("api/employee")]
        public IHttpActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            // Validate the incoming employee DTO
            if (employeeDto == null)
                return BadRequest("Invalid employee data.");

            // Add the employee using the service
            empService.AddEmployee(employeeDto);

            return Ok(new { Message = "Employee added successfully", EmployeeId = employeeDto.Id });
        }

       
        [HttpPut]
        [Route("api/employee/{id}")]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
        {
            // Validate the employee DTO and check if the ID matches
            if (employeeDto == null || id != employeeDto.Id)
                return BadRequest("Employee data is invalid.");

            // Update the employee using the service
            empService.UpdateEmployee(employeeDto);

            return Ok(new { Message = "Employee updated successfully." });
        }

       
        [HttpDelete]
        [Route("api/employee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            // Check if the employee exists before trying to delete
            var employee = empService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();  

            empService.DeleteEmployee(id);
            return Ok(new { Message = "Employee deleted successfully." });
        }

       
        [HttpGet]
        [Route("api/employee/search")]
        public IHttpActionResult Search([FromUri] string name = null, [FromUri] string salary = null, [FromUri] int? id = null, [FromUri] string joinDate = null)
        {
            // Search for employees based on provided query parameters
            var employees = empService.SearchEmployees(name, salary, id, joinDate);

            if (employees.Any())
                return Ok(new { Message = "Employees found.", Data = employees });

            return NotFound();
        }
    }
}
