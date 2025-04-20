using System.ComponentModel.DataAnnotations;

namespace TripWise.Domain.Entities
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required, StringLength(100)]
        public string CityName { get; set; }

        public int CountryId { get; set; }  // Changed to int and renamed to match schema (country_id)

        public virtual Country Country { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }

        public virtual ICollection<TransportCompany> TransportCompanies { get; set; }
    }
}