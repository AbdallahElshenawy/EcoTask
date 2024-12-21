using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task1.API.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }

}
