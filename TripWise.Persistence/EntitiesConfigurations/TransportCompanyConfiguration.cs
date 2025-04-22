using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripWise.Domain.Entities;

namespace TripWise.EntityFrameworkCore
{
    public class TransportCompanyConfiguration : IEntityTypeConfiguration<TransportCompany>
    {
        public void Configure(EntityTypeBuilder<TransportCompany> builder)
        {
            builder.HasKey(tc => tc.CompanyId);
            builder.Property(tc => tc.CompanyName).IsRequired().HasMaxLength(100);
            builder.Property(tc => tc.HQAddress).IsRequired().HasMaxLength(255);

            builder.HasOne(tc => tc.City)
                   .WithMany(c => c.TransportCompanies)
                   .HasForeignKey(tc => tc.CityId);

            builder.HasOne(tc => tc.CompanyType)
                   .WithMany(ct => ct.TransportCompanies)
                   .HasForeignKey(tc => tc.CompanyTypeId);
        }
    }
}
