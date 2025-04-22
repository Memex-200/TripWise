using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.EntityFrameworkCore.EntitiesConfigurations
{
    public class PromoOfferTransportServiceConfiguration : IEntityTypeConfiguration<PromoOfferTransportService>
    {
        public void Configure(EntityTypeBuilder<PromoOfferTransportService> builder)
        {
            builder.HasKey(pots => new { pots.PromoOfferId, pots.TransportServiceId });
            builder.HasOne(pots => pots.PromoOffer)
                   .WithMany(po => po.PromoOfferTransportServices)
                   .HasForeignKey(pots => pots.PromoOfferId);
            builder.HasOne(pots => pots.TransportService)
                   .WithMany()
                   .HasForeignKey(pots => pots.TransportServiceId);
        }
    }
}
