using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Domain.Entities;
using TripWise.Persistence;

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
            => await _context.Hotels.ToListAsync();

        public async Task<Hotel> GetHotelByIdAsync(int id)
            => await _context.Hotels.FindAsync(id);
    }
}
