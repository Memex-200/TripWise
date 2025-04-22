using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Domain.Entities;
using TripWise.EntityFrameworkCore;


namespace TripWise.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.City)
                    .ThenInclude(c => c.Country)
                .Include(h => h.HotelServices)
                    .ThenInclude(hs => hs.RoomType)
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.City)
                    .ThenInclude(c => c.Country)
                .Include(h => h.HotelServices)
                    .ThenInclude(hs => hs.RoomType)
                .FirstOrDefaultAsync(h => h.HotelId == id);
        }
    }
}
