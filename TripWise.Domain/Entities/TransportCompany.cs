using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class TransportCompany
    {
        [Key]
        public int CompanyId { get; set; }

        [Required, StringLength(100)]
        public string CompanyName { get; set; }

        [Required, StringLength(255)]
        public string HQAddress { get; set; }

        public string Description { get; set; }

        public bool IsPartner { get; set; }

        public bool Active { get; set; }

        public string Image { get; set; }

        public int CityId { get; set; }

        public int CompanyTypeId { get; set; }

        public virtual City City { get; set; }

        public virtual CompanyType CompanyType { get; set; }

        public virtual ICollection<TransportService> TransportServices { get; set; }
    }
}
