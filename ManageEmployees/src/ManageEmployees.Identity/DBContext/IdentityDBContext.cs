using ManageEmployees.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Identity.DBContext
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityDBContext).Assembly);
        }
    }
}
