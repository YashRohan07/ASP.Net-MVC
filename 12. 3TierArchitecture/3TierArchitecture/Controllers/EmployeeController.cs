using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace _3TierArchitecture.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService empService = new EmployeeService();

        [HttpGet]
        [Route("api/employee/getall")]
        public IHttpActionResult GetAllEmployees()
        {
            var employees = empService.GetAllEmployees();
            if (employees.Any())
                return Ok(new { Message = "Employees retrieved successfully.", Data = employees });

            return NotFound();
        }


        [HttpGet]
        [Route("api/employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var employee = empService.GetEmployeeById(id);
            if (employee != null)
                return Ok(new { Message = "Employee retrieved successfully.", Data = employee });

            return NotFound();
        }


        [HttpPost]
        [Route("api/employee")]
        public IHttpActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Invalid employee data.");

            empService.AddEmployee(employeeDto);
            return Ok(new { Message = "Employee added successfully", EmployeeId = employeeDto.Id });
        }


        [HttpPut]
        [Route("api/employee/{id}")]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null || id != employeeDto.Id)
                return BadRequest("Employee data is invalid.");

            empService.UpdateEmployee(employeeDto);
            return Ok(new { Message = "Employee updated successfully." });
        }


        [HttpDelete]
        [Route("api/employee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            if (empService.DeleteEmployee(id))
                return Ok(new { Message = "Employee deleted successfully." });

            return NotFound();
        }


        [HttpGet]
        [Route("api/employee/search")]
        public IHttpActionResult Search([FromUri] string name = null, [FromUri] string salary = null, [FromUri] int? id = null, [FromUri] string joinDate = null)
        {
            var employees = empService.SearchEmployees(name, salary, id, joinDate);
            if (employees.Any())
                return Ok(new { Message = "Employees found.", Data = employees });

            return NotFound();
        }
    }
}
