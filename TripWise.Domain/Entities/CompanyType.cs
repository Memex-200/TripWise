using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class CompanyType
    {
        [Key]
        public int CompanyTypeId { get; set; }

        [Required, StringLength(100)]
        public string TypeName { get; set; }

        public virtual ICollection<TransportCompany> TransportCompanies { get; set; }
    }
}
