using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.EntityFramework.Models;

namespace TripWise.Core.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required, StringLength(100)]
        public string CityName { get; set; }

        [Required, StringLength(3)]
        public string CountryCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }

        public virtual ICollection<TransportCompany> TransportCompanies { get; set; }
    }
}
