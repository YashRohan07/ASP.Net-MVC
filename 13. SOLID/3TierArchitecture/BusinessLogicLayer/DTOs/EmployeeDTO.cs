using System;
using AutoMapper;
namespace BusinessLogicLayer.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }  
        public DateTime JoinDate { get; set; }
    }
}
