using ManageEmployees.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageEmployees.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "9107d66c-2b0c-4023-83d8-eb0a77a9d631",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "SystemAdmin",
                    NormalizedUserName = "SYSTEMADMIN",
                    PasswordHash = hasher.HashPassword(null, "AdminPassw@rd"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "cd0ce604-ee45-4f29-84dc-10a14df9d0ed",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "SystemUser",
                    NormalizedUserName = "SYSTEMUSER",
                    PasswordHash = hasher.HashPassword(null, "UserPassw@rd"),
                    EmailConfirmed = true,
                }
            );
        }
    }
}
