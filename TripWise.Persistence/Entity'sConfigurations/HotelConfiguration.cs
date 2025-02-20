using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Entity_sConfigurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.HotelId);
            builder.Property(h => h.HotelName).IsRequired().HasMaxLength(100);
            builder.Property(h => h.HotelAddress).IsRequired().HasMaxLength(255);
            builder.Property(h => h.Active).HasDefaultValue(true);

            builder.HasOne(h => h.City)
                   .WithMany(c => c.Hotels)
                   .HasForeignKey(h => h.CityId);
        }
    }
}
