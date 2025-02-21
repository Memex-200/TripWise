using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Requires authentication
    public class BookingsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingRequest request)
        {
            // TODO: Implement actual booking logic (save to DB, integrate payment)
            return Ok(new { message = "Booking successful", request });
        }
    }

    public class BookingRequest
    {
        public int OfferId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
