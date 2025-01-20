using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Repos;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService
    {
        private readonly EmpRepos empRepos;
        private readonly IMapper mapper;

        public EmployeeService()
        {
            // Initialize dependencies
            var context = new Context();
            empRepos = new EmpRepos(context);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap());
            mapper = config.CreateMapper();
        }

        public void AddEmployee(EmployeeDTO employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            empRepos.AddEmployee(employee);
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            return mapper.Map<EmployeeDTO>(empRepos.GetEmployeeById(id));
        }


        public List<EmployeeDTO> GetAllEmployees()
        {
            var employees = empRepos.GetAllEmployees();
            return mapper.Map<List<EmployeeDTO>>(employees);
        }

        public void UpdateEmployee(EmployeeDTO employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            empRepos.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(int id)
        {
            return empRepos.DeleteEmployee(id);
        }

        public List<EmployeeDTO> SearchEmployees(string name, string salary, int? id, string joinDate)
        {
            var employees = empRepos.SearchEmployees(name, salary, id, joinDate);
            return mapper.Map<List<EmployeeDTO>>(employees);
        }
    }
}
