using System.ComponentModel.DataAnnotations;

namespace TripWise.Domain.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }  // Renamed to match schema (country_id)

        [Required, StringLength(100)]
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}