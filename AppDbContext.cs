using Microsoft.EntityFrameworkCore;
using NotificationSuite.Models;

namespace NotificationSuite
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<EmployeeDevice> EmployeeDevices { get; set; }
    }
}
