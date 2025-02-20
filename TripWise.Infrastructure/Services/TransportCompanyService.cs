using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;

namespace TripWise.Infrastructure.Services
{
    public class TransportCompanyService : ITransportCompanyService
    {
        private readonly ITransportCompanyRepository _companyRepository;

        public TransportCompanyService(ITransportCompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<TransportCompany>> GetAllCompaniesAsync()
            => await _companyRepository.GetAllCompaniesAsync();

        public async Task<TransportCompany> GetCompanyByIdAsync(int id)
            => await _companyRepository.GetCompanyByIdAsync(id);
    }
}