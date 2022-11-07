using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public partial class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<People>? People { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<EmployeeHistory>? EmployeeHistories { get; set; }

    }
}
