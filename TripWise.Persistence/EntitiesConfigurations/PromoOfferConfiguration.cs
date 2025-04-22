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
    public class PromoOfferConfiguration : IEntityTypeConfiguration<PromoOffer>
    {
        public void Configure(EntityTypeBuilder<PromoOffer> builder)
        {
            builder.HasKey(po => po.PromoOfferCode);
            builder.Property(po => po.PromoOfferName).IsRequired().HasMaxLength(100);
        }
    }
}
