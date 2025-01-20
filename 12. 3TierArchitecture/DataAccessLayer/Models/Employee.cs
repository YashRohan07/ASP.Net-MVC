using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
