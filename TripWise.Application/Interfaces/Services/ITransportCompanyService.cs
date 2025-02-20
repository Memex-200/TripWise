using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.Application.Interfaces.Services
{
    public interface ITransportCompanyService
    {
        Task<IEnumerable<TransportCompany>> GetAllCompaniesAsync();
        Task<TransportCompany> GetCompanyByIdAsync(int id);
    }
}
