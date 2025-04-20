namespace TripWise_API.DTOs
{
    public class OfferDetailsDto
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime ActiveFrom { get; set; }
        public bool ActiveTo { get; set; }
        public DateTime? TimeAccepted { get; set; }
        public bool IsAccepted { get; set; }

        public TransportCompanyDto TransportCompany { get; set; }
        public HotelServiceDto HotelService { get; set; }
        public PromoOfferDto PromoOffer { get; set; }
        public CustomerDto Customer { get; set; }
    }

    public class TransportCompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string HqAddress { get; set; }  // Matches HQAddress in TransportCompany entity
        public string Description { get; set; }
        public decimal TransportServicePrice { get; set; }
    }

    public class HotelServiceDto
    {
        public int HotelId { get; set; }  // Matches HotelServiceId in HotelService entity
        public HotelDto Hotel { get; set; }
        public decimal Price { get; set; }  // Matches ServicePrice in HotelService entity
        public decimal FinalServicePrice { get; set; }
    }

    public class HotelDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public CityDto City { get; set; }
    }

    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public CountryDto Country { get; set; }
    }

    public class CountryDto
    {
        public int CountryId { get; set; }  // Updated from CountryCode to CountryId
        public string CountryName { get; set; }
    }

    public class PromoOfferDto
    {
        public int PromoOfferId { get; set; }  // Matches PromoOfferCode in PromoOffer entity
        public string PromoName { get; set; }  // Matches PromoOfferName in PromoOffer entity
        public decimal DiscountPercent { get; set; }
        public decimal FinalServicePrice { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }  // Matches Id from IdentityUser<int>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}