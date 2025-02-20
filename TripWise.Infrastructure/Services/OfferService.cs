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
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
            => await _offerRepository.GetAllOffersAsync();

        public async Task<Offer> GetOfferByIdAsync(int id)
            => await _offerRepository.GetOfferByIdAsync(id);
    }
}
