using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageEmployees.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1cf8a3f0-f2fa-4ab7-95fa-ad13306028a3",
                    UserId = "9107d66c-2b0c-4023-83d8-eb0a77a9d631",
                },
                new IdentityUserRole<string>
                {
                    RoleId = "112b0b44-074e-45ab-baca-c7d3eefbcbb6",
                    UserId = "cd0ce604-ee45-4f29-84dc-10a14df9d0ed",
                }
            );
        }
    }
}
