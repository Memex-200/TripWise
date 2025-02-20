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
    public class HotelServiceConfiguration : IEntityTypeConfiguration<HotelService>
    {
        public void Configure(EntityTypeBuilder<HotelService> builder)
        {
            builder.ToTable("Hotel_Service");
            builder.HasOne(hs => hs.Hotel)
                   .WithMany(h => h.HotelServices)
                   .HasForeignKey(hs => hs.HotelId);

            builder.HasOne(hs => hs.RoomType)
                   .WithMany(rt => rt.HotelServices)
                   .HasForeignKey(hs => hs.RoomTypeId);
        }
    }
}
