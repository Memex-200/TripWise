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
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
            => await _hotelRepository.GetAllHotelsAsync();

        public async Task<Hotel> GetHotelByIdAsync(int id)
            => await _hotelRepository.GetHotelByIdAsync(id);
    }
}
