using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripWise.Domain.Entities;
using TripWise.Persistence.Entity_sConfigurations;

namespace TripWise.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Customer, IdentityRole<int>, int>
    {
        // Constructor that accepts DbContextOptions and passes it to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        // ✅ Define DbSets for your application
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TransportService> TransportServices { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<PromoOffer> PromoOffers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PromoOfferTransportService> PromoOfferTransportServices { get; set; }
        public DbSet<PromoOfferHotelService> PromoOfferHotelServices { get; set; }
        public DbSet<OfferTransportService> OfferTransportServices { get; set; }
        public DbSet<OfferHotelService> OfferHotelServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Apply Identity configurations
            base.OnModelCreating(modelBuilder);

            // ✅ Apply entity-specific configurations
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

            // ✅ Customize Identity Table Names (Optional)
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
