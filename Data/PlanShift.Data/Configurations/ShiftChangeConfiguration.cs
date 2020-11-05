namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class ShiftChangeConfiguration : IEntityTypeConfiguration<ShiftChange>
    {
        public void Configure(EntityTypeBuilder<ShiftChange> builder)
        {
            builder
                .HasOne(sc => sc.Shift)
                .WithMany(s => s.ShiftChanges)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sc => sc.OriginalEmployee)
                .WithMany(e => e.ChangedShifts)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sc => sc.PendingEmployee)
                .WithMany(e => e.TakenShifts)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sc => sc.Management)
                .WithMany(e => e.ManagedShifts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
