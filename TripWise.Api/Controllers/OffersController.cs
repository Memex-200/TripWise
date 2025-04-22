using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise_API.DTOs;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffers()
        {
            var offers = await _offerService.GetAllOffersAsync();

            var offerDtos = offers
                 .Where(o => o.ActiveTo >= DateTime.UtcNow)
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

                    TransportCompany = o.TransportCompany == null ? null : new TransportCompanyDto
                    {
                        CompanyId = o.TransportCompany.CompanyId,
                        CompanyName = o.TransportCompany.CompanyName,
                        HqAddress = o.TransportCompany.HQAddress,
                        Description = o.TransportCompany.Description,
                        TransportServicePrice = o.TransportCompany.TransportServicePrice
                    },

                    HotelService = o.HotelService == null ? null : new HotelServiceDto
                    {
                        HotelId = o.HotelService.HotelServiceId,
                        Price = o.HotelService.ServicePrice,
                        FinalServicePrice = o.HotelService.FinalServicePrice,
                        Hotel = o.HotelService.Hotel == null ? null : new HotelDto
                        {
                            HotelId = o.HotelService.Hotel.HotelId,
                            HotelName = o.HotelService.Hotel.HotelName,
                            City = o.HotelService.Hotel.City == null ? null : new CityDto
                            {
                                CityId = o.HotelService.Hotel.City.CityId,
                                CityName = o.HotelService.Hotel.City.CityName,
                                Country = o.HotelService.Hotel.City.Country == null ? null : new CountryDto
                                {
                                    CountryId = o.HotelService.Hotel.City.Country.CountryId,
                                    CountryName = o.HotelService.Hotel.City.Country.CountryName
                                }
                            }
                        }
                    },

                    PromoOffer = o.PromoOffer == null ? null : new PromoOfferDto
                    {
                        PromoOfferId = o.PromoOffer.PromoOfferCode,
                        PromoName = o.PromoOffer.PromoOfferName,
                        DiscountPercent = 0, // Replace with real logic if needed
                        FinalServicePrice = 0
                    },

                    Customer = o.Customer == null ? null : new CustomerDto
                    {
                        CustomerId = o.Customer.Id,
                        FirstName = o.Customer.FirstName,
                        LastName = o.Customer.LastName,
                        Address = o.Customer.Address,
                        Phone = o.Customer.Phone,
                        Email = o.Customer.Email
                    }
                })
                .ToList();

         /*   if (!offerDtos.Any())
                return NotFound("No active offers found.");
         */
            return Ok(offerDtos);
        }
    }
}
