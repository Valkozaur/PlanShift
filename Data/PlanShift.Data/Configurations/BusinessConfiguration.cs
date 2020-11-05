namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
