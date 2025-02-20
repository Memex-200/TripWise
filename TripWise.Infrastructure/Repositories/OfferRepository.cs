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
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationDbContext _context;

        public OfferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
            => await _context.Offers.ToListAsync();

        public async Task<Offer> GetOfferByIdAsync(int id)
            => await _context.Offers.FindAsync(id);
    }
}
