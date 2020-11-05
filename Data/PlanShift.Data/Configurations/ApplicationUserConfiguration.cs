namespace PlanShift.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlanShift.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<PlanShiftUser>
    {
        public void Configure(EntityTypeBuilder<PlanShiftUser> builder)
        {
            builder
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(x => x.UserName)
                .IsUnique();
        }
    }
}
