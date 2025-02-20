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
    public class TransportServiceConfiguration : IEntityTypeConfiguration<TransportService>
    {
        public void Configure(EntityTypeBuilder<TransportService> builder)
        {
            builder.ToTable("Transport_Service");
            builder.HasOne(ts => ts.TransportCompany)
                   .WithMany(tc => tc.TransportServices)
                   .HasForeignKey(ts => ts.TransportCompanyId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ts => ts.TicketType)
                   .WithMany(tt => tt.TransportServices)
                   .HasForeignKey(ts => ts.TicketTypeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ts => ts.FromCity)
                   .WithMany()
                   .HasForeignKey(ts => ts.FromCityId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ts => ts.ToCity)
                   .WithMany()
                   .HasForeignKey(ts => ts.ToCityId)
                   .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
