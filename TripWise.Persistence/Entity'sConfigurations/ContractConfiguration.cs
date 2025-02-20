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
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.ContractCode);

            builder.Property(c => c.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(c => c.PaymentAmount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.RefundedAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.Customer)
                   .WithMany(cu => cu.Contracts)
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Agent)
                   .WithMany(a => a.Contracts)
                   .HasForeignKey(c => c.AgentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Offer)
                   .WithMany(o => o.Contracts)
                   .HasForeignKey(c => c.OfferCode)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
