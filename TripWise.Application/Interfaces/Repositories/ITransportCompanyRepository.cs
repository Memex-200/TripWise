using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.Application.Interfaces.Repositories
{
    public interface ITransportCompanyRepository
    {
        Task<IEnumerable<TransportCompany>> GetAllCompaniesAsync();
        Task<TransportCompany> GetCompanyByIdAsync(int id);
    }
}
