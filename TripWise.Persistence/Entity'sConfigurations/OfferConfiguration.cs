using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Entity_sConfigurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.OfferId);

            builder.Property(o => o.OfferName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Description).HasMaxLength(255);
            builder.Property(o => o.IsAccepted).HasDefaultValue(false);

            //builder.HasOne(o => o.Agent)
            //       .WithMany(a => a.Offers)
            //       .HasForeignKey(o => o.AgentId)
            //       .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Offers)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.TransportCompany)
                   .WithMany(tc => tc.Offers)
                   .HasForeignKey(o => o.TransportCompanyId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.HotelService)
                   .WithMany(hs => hs.Offers)
                   .HasForeignKey(o => o.HotelServiceId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.PromoOffer)
                   .WithMany(po => po.Offers)
                   .HasForeignKey(o => o.PromoOfferId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
