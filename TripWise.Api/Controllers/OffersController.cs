using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise_API.DTOs;
using System.Linq;
using System.Threading.Tasks;

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
                .Select(o => {
                    // Calculate the discount percentage
                    int discountPercent = 0;
                    decimal finalPrice = 0;

                    // Calculate based on the difference between original and final price in HotelService
                    if (o.HotelService != null && o.HotelService.ServicePrice > 0)
                    {
                        // Calculate the discount percent based on the difference between original and final price
                        decimal originalPrice = o.HotelService.ServicePrice;
                        decimal finalHotelPrice = o.HotelService.FinalServicePrice;

                        if (originalPrice > finalHotelPrice)
                        {
                            decimal discountAmount = originalPrice - finalHotelPrice;
                            discountPercent = (int)Math.Round((discountAmount / originalPrice) * 100);
                            finalPrice = finalHotelPrice;
                        }
                        else
                        {
                            // If no discount is applied in HotelService
                            finalPrice = originalPrice;
                        }
                    }

                    // Create the DTO with calculated values
                    return new OfferDetailsDto
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
                            DiscountPercent = discountPercent, // Use the calculated discount percent
                            FinalServicePrice = finalPrice     // Use the calculated final price
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
                    };
                })
                .ToList();

            return Ok(offerDtos);
        }
    }
}