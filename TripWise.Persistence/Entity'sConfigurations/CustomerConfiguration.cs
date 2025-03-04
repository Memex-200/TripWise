using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Entity_sConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id); // ✅ Use `Id` instead of `CustomerId`

            builder.Property(c => c.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Email)
                   .HasMaxLength(100);

            builder.Property(c => c.Address)
                   .HasMaxLength(255);

            builder.Property(c => c.Phone)
                   .HasMaxLength(255);

            builder.Property(c => c.Mobile)
                   .HasMaxLength(255);

            builder.Property(c => c.CustomerFrom)
                   .HasDefaultValueSql("GETUTCDATE()"); // ✅ Default value for date

            builder.HasMany(c => c.Offers)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Contracts)
                   .WithOne(c => c.Customer)
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
