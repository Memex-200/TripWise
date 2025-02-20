using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.Persistence
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.OfferCode);
            builder.Property(o => o.OfferName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Accepted).HasDefaultValue(false);

            builder.HasOne(o => o.Agent)
                   .WithMany(a => a.Offers)
                   .HasForeignKey(o => o.AgentId);

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Offers)
                   .HasForeignKey(o => o.CustomerId);
        }
    }


}
