using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripWise.Application.Interfaces.Services;
using TripWise.Persistence;
using TripWise_API.DTOs;


namespace TripWise_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ApplicationDbContext _context;

        public OffersController(IOfferService offerService, ApplicationDbContext context)
        {
            _offerService = offerService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffersWithDetails()
        {
            try
            {
                var offers = await _context.Offers
                    .Include(o => o.TransportCompany)
                    .Include(o => o.HotelService).ThenInclude(hs => hs.Hotel).ThenInclude(h => h.City).ThenInclude(c => c.Country)
                    .Include(o => o.PromoOffer)
                    .Include(o => o.Customer)
                    .Where(o => o.ActiveTo == true)
                    .Select(o => new OfferDetailsDto
                    {
                        OfferId = o.OfferId,
                        OfferName = o.OfferName,
                        Description = o.Description,
                        Created = o.Created,
                        ActiveFrom = o.ActiveFrom,
                        ActiveTo = o.ActiveTo,
                        TimeAccepted = o.TimeAccepted,
                        IsAccepted = o.IsAccepted,
                        TransportCompany = o.TransportCompany != null ? new TransportCompanyDto
                        {
                            CompanyId = o.TransportCompany.CompanyId,
                            CompanyName = o.TransportCompany.CompanyName,
                            HqAddress = o.TransportCompany.HQAddress,
                            Description = o.TransportCompany.Description,
                            TransportServicePrice = o.TransportCompany.TransportServicePrice
                        } : null,
                        HotelService = o.HotelService != null ? new HotelServiceDto
                        {
                            HotelId = o.HotelService.HotelServiceId,
                            Hotel = o.HotelService.Hotel != null ? new HotelDto
                            {
                                HotelId = o.HotelService.Hotel.HotelId,
                                HotelName = o.HotelService.Hotel.HotelName,
                                City = o.HotelService.Hotel.City != null ? new CityDto
                                {
                                    CityId = o.HotelService.Hotel.City.CityId,
                                    CityName = o.HotelService.Hotel.City.CityName,
                                    Country = o.HotelService.Hotel.City.Country != null ? new CountryDto
                                    {
                                        CountryId = o.HotelService.Hotel.City.Country.CountryId,  // Already matches
                                        CountryName = o.HotelService.Hotel.City.Country.CountryName
                                    } : null
                                } : null
                            } : null,
                            Price = o.HotelService.ServicePrice,
                            FinalServicePrice = o.HotelService.FinalServicePrice
                        } : null,
                        PromoOffer = o.PromoOffer != null ? new PromoOfferDto
                        {
                            PromoOfferId = o.PromoOffer.PromoOfferCode,
                            PromoName = o.PromoOffer.PromoOfferName,
                           
                        } : null,
                        Customer = o.Customer != null ? new CustomerDto
                        {
                            CustomerId = o.Customer.Id,
                            FirstName = o.Customer.FirstName,
                            LastName = o.Customer.LastName,
                            Address = o.Customer.Address,
                            Phone = o.Customer.Phone,
                            Email = o.Customer.Email
                        } : null
                    })
                    .ToListAsync();

                if (offers == null || !offers.Any())
                {
                    return NotFound("No active offers found.");
                }

                return Ok(offers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}