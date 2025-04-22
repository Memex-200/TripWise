using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Domain.Entities;
using TripWise.EntityFrameworkCore;


namespace TripWise.Infrastructure.Repositories
{
    public class TransportCompanyRepository : ITransportCompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public TransportCompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransportCompany>> GetAllCompaniesAsync()
            => await _context.TransportCompanies.ToListAsync();

        public async Task<TransportCompany> GetCompanyByIdAsync(int id)
            => await _context.TransportCompanies.FindAsync(id);
    }
}
