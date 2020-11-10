namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class ShiftApplicationConfiguration : IEntityTypeConfiguration<ShiftApplication>
    {
        public void Configure(EntityTypeBuilder<ShiftApplication> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}