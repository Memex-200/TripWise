using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
            => Ok(await _hotelService.GetAllHotelsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            return hotel == null ? NotFound() : Ok(hotel);
        }
    }
}
