using TripWise.Domain.Entities;

namespace TripWise.Application.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
    }
}
