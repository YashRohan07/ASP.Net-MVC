using System;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }  
        public DateTime JoinDate { get; set; }
    }
}
