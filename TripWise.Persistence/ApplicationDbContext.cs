using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripWise.Domain.Entities;
using TripWise.Persistence.Entity_sConfigurations;

namespace TripWise.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Customer, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        // Define DbSets
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TransportService> TransportServices { get; set; }
        public DbSet<PromoOffer> PromoOffers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PromoOfferTransportService> PromoOfferTransportServices { get; set; }
        public DbSet<PromoOfferHotelService> PromoOfferHotelServices { get; set; }
        // Removed OfferHotelService and OfferTransportService to align with one-to-one relationships

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity-specific configurations
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new TransportCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new TransportServiceConfiguration());
            modelBuilder.ApplyConfiguration(new HotelServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PromoOfferHotelServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PromoOfferTransportServiceConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());

            // Configure relationships for entities without configurations
            // RoomType
            modelBuilder.Entity<RoomType>()
                .HasKey(rt => rt.RoomTypeId);

            modelBuilder.Entity<HotelService>()
                .HasOne(hs => hs.RoomType)
                .WithMany()
                .HasForeignKey(hs => hs.RoomTypeId);

            // CompanyType
            modelBuilder.Entity<CompanyType>()
                .HasKey(ct => ct.CompanyTypeId);

            modelBuilder.Entity<TransportCompany>()
                .HasOne(tc => tc.CompanyType)
                .WithMany()
                .HasForeignKey(tc => tc.CompanyTypeId);

            // TicketType
            modelBuilder.Entity<TicketType>()
                .HasKey(tt => tt.TicketTypeId);

            modelBuilder.Entity<TransportService>()
                .HasOne(ts => ts.TicketType)
                .WithMany()
                .HasForeignKey(ts => ts.TicketTypeId);

            // PromoOffer
            modelBuilder.Entity<PromoOffer>()
                .HasKey(po => po.PromoOfferCode);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.PromoOffer)
                .WithMany(po => po.Offers)
                .HasForeignKey(o => o.PromoOfferId);

            // Additional relationships
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.TransportCompany)
                .WithMany(tc => tc.Offers)
                .HasForeignKey(o => o.TransportCompanyId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.HotelService)
                .WithMany(hs => hs.Offers)
                .HasForeignKey(o => o.HotelServiceId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<HotelService>()
                .HasOne(hs => hs.Hotel)
                .WithMany()
                .HasForeignKey(hs => hs.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany()
                .HasForeignKey(h => h.CityId);

            modelBuilder.Entity<City>()
        .HasOne(c => c.Country)
        .WithMany(co => co.Cities)
        .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<TransportCompany>()
                .HasOne(tc => tc.City)
                .WithMany()
                .HasForeignKey(tc => tc.CityId);

            // Customize Identity Table Names
            modelBuilder.Entity<Customer>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");
        }
    }
}