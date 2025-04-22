using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Domain.Entities;
using TripWise.EntityFrameworkCore;


namespace TripWise.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationDbContext _context;

        public OfferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // improve include performance (select)
        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
        {
            return await _context.Offers
                .Include(o => o.HotelService)
                    .ThenInclude(hs => hs.Hotel)
                        .ThenInclude(h => h.City)
                            .ThenInclude(c => c.Country)
                .Include(o => o.HotelService)
                    .ThenInclude(hs => hs.RoomType)
                .Include(o => o.TransportCompany)
                    .ThenInclude(tc => tc.City)
                        .ThenInclude(c => c.Country)
                .Include(o => o.TransportCompany)
                    .ThenInclude(tc => tc.CompanyType)
                .Include(o => o.PromoOffer)
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public async Task<Offer> GetOfferByIdAsync(int id)
        {
            return await _context.Offers
                .Include(o => o.HotelService)
                    .ThenInclude(hs => hs.Hotel)
                        .ThenInclude(h => h.City)
                            .ThenInclude(c => c.Country)
                .Include(o => o.HotelService)
                    .ThenInclude(hs => hs.RoomType)
                .Include(o => o.TransportCompany)
                    .ThenInclude(tc => tc.City)
                        .ThenInclude(c => c.Country)
                .Include(o => o.TransportCompany)
                    .ThenInclude(tc => tc.CompanyType)
                .Include(o => o.PromoOffer)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.OfferId == id);
        }
    }
}
