using Microsoft.EntityFrameworkCore;
using TripWise.Core.Models;
using TripWise.EntityFramework.Models;

namespace TripWise.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes it to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TransportService> TransportServices { get; set; }
        public DbSet<Customer> Customers { get; set; }
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
            modelBuilder.Entity<Country>()
                .ToTable("Country")
                .HasKey(c => c.CountryCode);

            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryCode);

            modelBuilder.Entity<Hotel>()
                .ToTable("Hotels")
                .HasOne(h => h.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CityId);

            modelBuilder.Entity<TransportCompany>()
                .ToTable("Transport_Company")
                .HasOne(tc => tc.City)
                .WithMany(c => c.TransportCompanies)
                .HasForeignKey(tc => tc.CityId);

            modelBuilder.Entity<TransportCompany>()
                .HasOne(tc => tc.CompanyType)
                .WithMany(ct => ct.TransportCompanies)
                .HasForeignKey(tc => tc.CompanyTypeId);

            modelBuilder.Entity<TransportService>()
      .ToTable("Transport_Service")
      .HasOne(ts => ts.TransportCompany)
      .WithMany(tc => tc.TransportServices)
      .HasForeignKey(ts => ts.TransportCompanyId)
      .OnDelete(DeleteBehavior.NoAction);  // Explicitly set delete behavior to prevent cascade issues

            modelBuilder.Entity<TransportService>()
                .HasOne(ts => ts.TicketType)
                .WithMany(tt => tt.TransportServices)
                .HasForeignKey(ts => ts.TicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);  // Explicitly set delete behavior to prevent cascade issues

            // Update city relationships to prevent multiple cascade paths
            modelBuilder.Entity<TransportService>()
                .HasOne(ts => ts.FromCity)
                .WithMany()
                .HasForeignKey(ts => ts.FromCityId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for FromCityId

            modelBuilder.Entity<TransportService>()
                .HasOne(ts => ts.ToCity)
                .WithMany()
                .HasForeignKey(ts => ts.ToCityId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete for ToCityId



            modelBuilder.Entity<HotelService>()
                .ToTable("Hotel_Service")
                .HasOne(hs => hs.Hotel)
                .WithMany(h => h.HotelServices)
                .HasForeignKey(hs => hs.HotelId);

            modelBuilder.Entity<HotelService>()
                .HasOne(hs => hs.RoomType)
                .WithMany(rt => rt.HotelServices)
                .HasForeignKey(hs => hs.RoomTypeId);

            modelBuilder.Entity<PromoOfferHotelService>()
                .HasKey(pohs => new { pohs.PromoOfferId, pohs.HotelServiceId });

            modelBuilder.Entity<PromoOfferHotelService>()
                .HasOne(pohs => pohs.PromoOffer)
                .WithMany(po => po.PromoOfferHotelServices)
                .HasForeignKey(pohs => pohs.PromoOfferId);

            modelBuilder.Entity<PromoOfferHotelService>()
                .HasOne(pohs => pohs.HotelService)
                .WithMany(hs => hs.PromoOffers)
                .HasForeignKey(pohs => pohs.HotelServiceId);

            modelBuilder.Entity<PromoOfferTransportService>()
                .HasKey(pots => new { pots.PromoOfferId, pots.TransportServiceId });

            modelBuilder.Entity<PromoOfferTransportService>()
                .HasOne(pots => pots.PromoOffer)
                .WithMany(po => po.PromoOfferTransportServices)
                .HasForeignKey(pots => pots.PromoOfferId);

            modelBuilder.Entity<PromoOfferTransportService>()
                .HasOne(pots => pots.TransportService)
                .WithMany()
                .HasForeignKey(pots => pots.TransportServiceId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.PromoOffer)
                .WithMany()
                .HasForeignKey(o => o.PromoOfferId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Agent)
                .WithMany()
                .HasForeignKey(o => o.AgentId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // You can keep Cascade or use SetNull if needed

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Agent)
                .WithMany()
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.Cascade);  // You can keep Cascade or use SetNull if needed

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Offer)
                .WithMany()
                .HasForeignKey(c => c.OfferCode)
                .OnDelete(DeleteBehavior.NoAction);  // Use NoAction to avoid cascade conflicts for OfferCode

            base.OnModelCreating(modelBuilder);
        }
    }
}
