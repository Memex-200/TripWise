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
    public class PromoOfferHotelServiceConfiguration : IEntityTypeConfiguration<PromoOfferHotelService>
    {
        public void Configure(EntityTypeBuilder<PromoOfferHotelService> builder)
        {
            builder.HasKey(pohs => new { pohs.PromoOfferId, pohs.HotelServiceId });
            builder.HasOne(pohs => pohs.PromoOffer)
                   .WithMany(po => po.PromoOfferHotelServices)
                   .HasForeignKey(pohs => pohs.PromoOfferId);
            builder.HasOne(pohs => pohs.HotelService)
                   .WithMany(hs => hs.PromoOffers)
                   .HasForeignKey(pohs => pohs.HotelServiceId);
        }
    }
}
