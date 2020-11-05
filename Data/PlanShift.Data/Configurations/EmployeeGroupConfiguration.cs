namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class EmployeeGroupConfiguration : IEntityTypeConfiguration<EmployeeGroup>
    {
        public void Configure(EntityTypeBuilder<EmployeeGroup> builder)
        {
            builder
                .HasOne(eg => eg.Group)
                .WithMany(g => g.Employees)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
