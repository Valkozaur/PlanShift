namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> appUser)
        {
            appUser
                .HasOne(s => s.Employee)
                .WithMany(e => e.Shifts)
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasOne(s => s.Group)
                .WithMany(g => g.Shifts)
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasOne(s => s.ShiftCreator)
                .WithMany(e => e.CreatedShifts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
