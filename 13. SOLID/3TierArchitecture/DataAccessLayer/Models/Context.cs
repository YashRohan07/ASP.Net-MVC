using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

    }
}
