using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripWise.Domain.Entities;
using TripWise.EntityFrameworkCore;
using TripWise.EntityFrameworkCore.EntitiesConfigurations;

namespace TripWise.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<Customer, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        // DbSets
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from separate files
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelServiceConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
           // modelBuilder.ApplyConfiguration(new CompanyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransportCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new TransportServiceConfiguration());
            modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PromoOfferConfiguration());
            modelBuilder.ApplyConfiguration(new PromoOfferHotelServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PromoOfferTransportServiceConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            // Customize Identity table names
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
