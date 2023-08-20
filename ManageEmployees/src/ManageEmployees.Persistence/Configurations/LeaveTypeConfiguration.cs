using ManageEmployees.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageEmployees.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);

            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDays = 10,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Sick",
                    DefaultDays = 10,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new LeaveType
                {
                    Id = 3,
                    Name = "Birthday",
                    DefaultDays = 1,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            );
        }
    }
}
