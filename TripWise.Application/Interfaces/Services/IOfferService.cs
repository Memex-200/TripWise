using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Domain.Entities;

namespace TripWise.Application.Interfaces.Services
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllOffersAsync();
        Task<Offer> GetOfferByIdAsync(int id);
    }
}
