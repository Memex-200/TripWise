using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Core.Models
{
    public class Country
    {
        [Key]
        [StringLength(3)]

        public string CountryCode { get; set; }

        [Required, StringLength(100)]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
