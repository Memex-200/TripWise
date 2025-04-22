using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripWise.Domain.Entities;

namespace TripWise.EntityFrameworkCore
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.CityId);
            builder.Property(c => c.CityName).IsRequired().HasMaxLength(100);

            builder.ToTable("City");
            builder.HasOne(c => c.Country)
                   .WithMany(co => co.Cities)
                   .HasForeignKey(c => c.CountryId);  // Updated to CountryId
        }
    }
}