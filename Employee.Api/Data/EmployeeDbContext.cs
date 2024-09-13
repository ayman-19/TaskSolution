using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options) { }

        public DbSet<Entities.Employee> Employees { get; set; }
    }
}
