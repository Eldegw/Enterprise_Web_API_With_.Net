using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
       
        public ApplicationContext(DbContextOptions options): base(options) 
        {

            
        }
    }
}
