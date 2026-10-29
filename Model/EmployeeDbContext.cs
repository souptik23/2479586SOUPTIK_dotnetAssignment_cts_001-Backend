using Microsoft.EntityFrameworkCore;

namespace EmployeeManagemantDotnetV1.Model
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext( DbContextOptions<EmployeeDbContext> options): base(options) 
        {
            
        } 

        public DbSet<Employee> Employees { get; set; }
    }
}
