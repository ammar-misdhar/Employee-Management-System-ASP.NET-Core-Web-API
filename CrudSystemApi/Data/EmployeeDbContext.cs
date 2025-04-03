using CrudSystemApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudSystemApi.Data
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext>options):base (options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
